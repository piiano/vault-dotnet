using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Vault.Tests;

public class CollectionsTests
{
    private ICollectionsClient _client = null!;
    private CollectionsHelper _collectionsHelper = null!;

    [SetUp]
    public async Task Setup()
    {
        _client = new ClientFactory().Collections;
        _collectionsHelper = new CollectionsHelper(_client);
            
        await _collectionsHelper.DeleteAllCollections();
    }

    [Test]
    public async Task TestListCollections()
    {
        var collections = (await _client.ListAsync())?.ToList();
            
        Assert.IsNotNull(collections);
        CollectionAssert.AllItemsAreNotNull(collections);
    }

    [Test]
    public async Task TestCrud()
    {
        await _collectionsHelper.DeleteAllCollections();

        Collection newCollection = await _collectionsHelper.AddNewCollection();
            
        await UpdateCollection(newCollection);
            
        Assert.True(await WasCollectionUpdated(newCollection));

        await _collectionsHelper.DeleteCollection(newCollection);
    }

    private async Task<bool> WasCollectionUpdated(Collection collection)
    {
        Collection c = await _client.GetAsync(new GetCollectionArgs
        {
            CollectionName = collection.Name
        });

        return CollectionComparer.Instance.Equals(c, collection);
    }

    private async Task<Collection> UpdateCollection(Collection collection)
    {
        collection.Properties = collection.Properties.Append(
            new Property
            {
                Name = "B",
                Description = "",
                Data_type_name = "STRING",
            });
            
        Collection updated = await _client.UpdateAsync(new UpdateCollectionArgs
        {
            Collection = collection,
            CollectionName = collection.Name
        });

        CollectionsHelper.AssertCollectionsEqual(collection, updated);
            
        return updated;
    }
}