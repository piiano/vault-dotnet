using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Vault.Tests;

public class CollectionsPropertiesTests
{
    private ICollectionsClient _collectionsClient = null!;
    private ICollectionPropertiesClient _propertyClient = null!;
    private CollectionsHelper _collectionsHelper = null!;

    [SetUp]
    public void Setup()
    {
        var clientFactory = new ClientFactory();
        
        _collectionsClient = clientFactory.Collections;
        _propertyClient = clientFactory.CollectionProperties;
        _collectionsHelper = new CollectionsHelper(_collectionsClient);
    }

    [Test]
    public async Task TestCrud()
    {
        Collection collection = await PrepareCollection();

        Property property = await AddProperty(collection);
        await WasPropertyUpdated(collection, property);

        property = await UpdateProperty(collection, property.Name);
        Assert.True(await WasPropertyUpdated(collection, property));

        await DeleteProperty(collection, property);
    }

    private async Task<Collection> PrepareCollection()
    {
        IEnumerable<Collection> collections = await _collectionsClient.ListAsync();

        Collection collection = collections.FirstOrDefault(c => c.Name == CollectionsHelper.TestCollectionName) ??
            await _collectionsHelper.AddNewCollection();

        IEnumerable<Property> collectionProperties = await _propertyClient
            .ListCollectionPropertiesAsync(
                new ListCollectionPropertiesArgs
                {
                    CollectionName = collection.Name
                });
        
        var property = collectionProperties
            .FirstOrDefault(p => p.Name == CollectionsHelper.TestPropertyName);

        if (property != null)
        {
            await DeleteProperty(collection, property);
        }

        return collection;
    }

    private async Task DeleteProperty(Collection collection, Property property)
    {
        await _propertyClient.DeleteCollectionPropertyAsync(new DeleteCollectionPropertyArgs
        {
            CollectionName = collection.Name,
            PropertyName = property.Name
        });

        Collection newCollection = await _collectionsClient.GetAsync(new GetCollectionArgs
        {
            CollectionName = collection.Name
        });

        Assert.False(newCollection.Properties.Any(p => p.Name == property.Name));
    }

    private async Task<Property> AddProperty(Collection collection)
    {
        var request = new Property
        {
            Name = CollectionsHelper.TestPropertyName,
            Description = "",
            Data_type_name = "STRING",
        };

        Property property = await _propertyClient.AddCollectionPropertyAsync(new AddCollectionPropertyArgs
        {
            CollectionName = collection.Name,
            PropertyName = request.Name,
            Property = request
        });

        Assert.True(PropertyComparer.Instance.Equals(request, property));

        return property;
    }

    private async Task<Property> UpdateProperty(Collection collection, string propertyName)
    {
        var request = new UpdatePropertyRequest
        {
            Description = "New description",
        };

        Property property = await _propertyClient.UpdateCollectionPropertyAsync(new UpdateCollectionPropertyArgs
        {
            CollectionName = collection.Name,
            PropertyName = propertyName,
            Property = request
        });

        Assert.AreEqual(request.Description, property.Description);

        return property;
    }

    private async Task<bool> WasPropertyUpdated(Collection collection, Property property)
    {
        var p = await _propertyClient.GetCollectionPropertyAsync(new GetCollectionPropertyArgs
        {
            CollectionName = collection.Name,
            PropertyName = property.Name
        });

        return PropertyComparer.Instance.Equals(property, p);
    }
}