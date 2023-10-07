using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Vault.Tests;

public class CollectionsHelper
{
    public const string TestCollectionName = "test1";
    public const string TestPropertyName = "C";

    private readonly ICollectionsClient _client;

    public CollectionsHelper(ICollectionsClient client)
    {
        _client = client;
    }

    public async Task<Collection> AddNewCollection()
    {
        var properties = Enumerable
            .Range(1, 6)
            .Select(
                i => new Property
                {
                    Name = string.Concat(Enumerable.Repeat(TestPropertyName, i)),
                    Description = "",
                    Data_type_name = "STRING",
                    Is_nullable = true
                });

        var request = new Collection
        {
            Name = TestCollectionName,
            Properties = properties,
            Type = CollectionType.PERSONS
        };

        Collection added = await _client.AddAsync(new AddCollectionArgs { Collection = request});

        AssertCollectionsEqual(request, added);

        return added;
    }

    public static void AssertCollectionsEqual(Collection x, Collection y)
    {
        Assert.True(CollectionComparer.Instance.Equals(x, y));
    }

    public async Task DeleteAllCollections()
    {
        await DeleteCollections(await _client.ListAsync(new ListCollectionsArgs()));
    }

    private async Task DeleteCollections(IEnumerable<Collection> collections)
    {
        foreach (Collection collection in collections)
        {
            await DeleteCollection(collection);
        }
    }

    public async Task DeleteCollection(Collection collection)
    {
        await _client.DeleteAsync(new DeleteCollectionArgs
        {
            CollectionName = collection.Name
        });

        IEnumerable<Collection> collections = await _client.ListAsync(new ListCollectionsArgs());

        Assert.False(collections.Any(c => c.Name == collection.Name));
    }
}