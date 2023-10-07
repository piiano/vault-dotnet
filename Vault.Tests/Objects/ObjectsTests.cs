using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Vault.Tests;

public class ObjectsTests
{
    private const string IdField = "id";
    private IObjectsClient _client = null!;
    private CollectionsHelper __collectionsHelper = null!;
    private Collection _collection = null!;

    [SetUp]
    public async Task Setup()
    {
        var clientFactory = new ClientFactory();
            
        _client = clientFactory.Objects;
        __collectionsHelper = new CollectionsHelper(clientFactory.Collections);
        
        await __collectionsHelper.DeleteAllCollections();
        _collection = await __collectionsHelper.AddNewCollection();
    }
        
    [Test]
    public async Task TestGetAllObjects()
    {
        Assert.NotNull(await _client.ListObjectsAsync(new ListObjectsUnsafeArgs
        {
            CollectionName = CollectionsHelper.TestCollectionName,
        }));
    }

    [Test]
    public async Task TestCrudWithSingleMethods()
    {
        await DeleteAllObjectsSingle();

        const string key = CollectionsHelper.TestPropertyName;
        string value = "Some string";

        var id = await AddObjectSingle(key, value);
        await ValidateObjectExistence(id, key, value);
            
        value = "Some different string";
        await UpdateObjectSingle(id, key, value);
        await ValidateObjectExistence(id, key, value);
            
        await DeleteAllObjectsSingle();
    }
        
    [Test]
    public async Task TestCrudWithBatchMethods()
    {
        await DeleteAllObjectsMultiple();

        const string key = CollectionsHelper.TestPropertyName;
        string value = "Some string";

        var id = await AddObjectBatch(key, value);
        await ValidateObjectExistence(id, key, value);
            
        value = "Some different string";
        await UpdateObjectBulk(id, key, value);
        await ValidateObjectExistence(id, key, value);
            
        await DeleteAllObjectsSingle();
    }

    private async Task UpdateObjectSingle(Guid id, string key, string value)
    {
        await _client.UpdateObjectByIdAsync(
            new UpdateObjectByIdArgs
            {
                CollectionName = _collection.Name,
                ObjectId = id,
                Object = new ObjectFields
                {
                    AdditionalProperties = new Dictionary<string, object>()
                    {
                        {key, value}
                    }
                }
            });
    }
    
    private async Task UpdateObjectBulk(Guid id, string key, string value)
    {
        await _client.UpdateObjectsAsync(
            new UpdateObjectsArgs
            {
                CollectionName = _collection.Name,
                Objects = new []
                {
                    new ObjectFields
                    {
                        AdditionalProperties = new Dictionary<string, object>()
                        {
                            {IdField, id},
                            {key, value}
                        }
                    }   
                }
            });
    }

    private async Task ValidateObjectExistence(Guid id, string key, string value)
    {
        string[] properties = { key };
        Guid[] objectIds = { id };
        var query = new Query
        {
            Match = new MatchMap {{key, value}}
        };
            
        var objects = await _client.ListObjectsAsync(new ListObjectsUnsafeArgs
        {
            CollectionName = _collection.Name,
        });
        AssertObjectsContainKeyValue(objects, key, value);

        objects = await _client.ListObjectsAsync(new ListObjectsUnsafeArgs
        {
            CollectionName = _collection.Name,
            ObjectIds = objectIds
        });
        AssertObjectsContainKeyValue(objects, key, value);

        objects = await _client.ListObjectsAsync(new ListObjectsPropsArgs
        {
            CollectionName = _collection.Name,
            Props = properties
        });
        AssertObjectsContainKeyValue(objects, key, value);

        objects = await _client.ListObjectsAsync(new ListObjectsPropsArgs
        {
            CollectionName = _collection.Name,
            ObjectIds = objectIds,
            Props = properties
        });
        AssertObjectsContainKeyValue(objects, key, value);
            
        objects = await _client.SearchObjectsAsync(new SearchObjectsUnsafeArgs
        {
            CollectionName = _collection.Name,
            Query = query
        });
        AssertObjectsContainKeyValue(objects, key, value);
            
        objects = await _client.SearchObjectsAsync(new SearchObjectsPropsArgs
        {
            CollectionName = _collection.Name,
            Query = query,
            Props = properties
        });
        AssertObjectsContainKeyValue(objects, key, value);

        var obj = await _client.GetObjectByIdAsync(new GetObjectByIdUnsafeArgs
        {
            CollectionName = _collection.Name,
            ObjectId = id
        });
        AssertObjectContainKeyValue(obj, key, value);
            
        obj = await _client.GetObjectByIdAsync(new GetObjectByIdPropsArgs
        {
            CollectionName = _collection.Name,
            ObjectId = id,
            Props = properties
        });
        AssertObjectContainKeyValue(obj, key, value);
    }

    private async Task<Guid> AddObjectSingle(string key, string value)
    {
        var result = await _client.AddObjectAsync(
            new AddObjectArgs
            {
                CollectionName = _collection.Name,
                Object = new ObjectFields
                {
                    AdditionalProperties = new Dictionary<string, object>
                    {
                        {key, value}
                    }
                }
            });
            
        return result.Id;
    }
    
    private async Task<Guid> AddObjectBatch(string key, string value)
    {
        BulkObjectResponse result = await _client.AddObjectsAsync(
            new AddObjectsArgs
            {
                CollectionName = _collection.Name,
                Objects = new []
                {
                    new ObjectFields
                    {
                        AdditionalProperties = new Dictionary<string, object>
                        {
                            {key, value}
                        }
                    }   
                }
            });
            
        Assert.True(result.Ok);
        
        BulkObjectResult bulkObjectResult = result.Results.First();
        Assert.True(bulkObjectResult.Ok);
        
        return bulkObjectResult.Id;
    }

    private static void AssertObjectsContainKeyValue(ObjectFieldsPage objects, string key, string value)
    {
        Assert.True(
            objects.Results.Any(
                obj => obj.AdditionalProperties.Contains(new KeyValuePair<string, object>(key, value))));
    }
        
    private static void AssertObjectContainKeyValue(ObjectFields obj, string key, string value)
    {
        Assert.True(obj.AdditionalProperties.Contains(new KeyValuePair<string, object>(key, value)));
    }

    private async Task DeleteAllObjectsSingle()
    {
        try
        {
            var objects = await _client.ListObjectsAsync(new ListObjectsUnsafeArgs
            {
                CollectionName = _collection.Name
            });
                
            foreach (var o in objects.Results)
            {
                await _client.DeleteObjectByIdAsync(
                    new DeleteObjectByIdArgs
                    {
                        CollectionName = _collection.Name,
                        ObjectId = new Guid(o.AdditionalProperties[IdField].ToString()!)
                    });
            }

            Count objectCount = await _client.GetObjectsCountAsync(new GetObjectsCountArgs
            {
                CollectionName = _collection.Name
            });
                
            Assert.AreEqual(0, objectCount.Total);
        }
        catch (ApiException e)
        {
            Assert.AreEqual(404, e.StatusCode);
        }
            
    }
    
    private async Task DeleteAllObjectsMultiple()
    {
        try
        {
            ObjectFieldsPage objects = await _client.ListObjectsAsync(new ListObjectsUnsafeArgs
            {
                CollectionName = _collection.Name
            });

            await _client.DeleteObjectsAsync(
                new DeleteObjectsArgs
                {
                    CollectionName = _collection.Name,
                    ObjectIds = objects.Results
                        .Select(o => new Guid(
                            o.AdditionalProperties[IdField].ToString()!))
                });

            Count objectCount = await _client.GetObjectsCountAsync(new GetObjectsCountArgs
            {
                CollectionName = _collection.Name
            });
                
            Assert.AreEqual(0, objectCount.Total);
        }
        catch (ApiException e)
        {
            Assert.AreEqual(404, e.StatusCode);
        }
            
    }
}