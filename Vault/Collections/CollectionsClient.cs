namespace Vault;

internal class CollectionsClient : ICollectionsClient
{
    private readonly IGeneratedClient _client;

    internal CollectionsClient(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<IEnumerable<Collection>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        return ListAsync(
            new ListCollectionsArgs(), cancellationToken);
    }

    public Task<IEnumerable<Collection>> ListAsync(
        ListCollectionsArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.ListCollectionsAsync(
            format: Format.Json,
            args.ShowBuiltins.ToOption<Anonymous>(),
            cancellationToken);
    }

    public Task<Collection> GetAsync(
        GetCollectionArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.GetCollectionAsync(
            args.CollectionName,
            format: Format.Json,
            args.ShowBuiltins.ToOption<Anonymous9>(),
            cancellationToken);
    }

    public Task<Collection> AddAsync(
        AddCollectionArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.AddCollectionAsync(
            format: Format.Json,
            args.ShowBuiltins.ToOption<Anonymous2>(),
            args.Collection,
            cancellationToken);
    }

    public Task<Collection> UpdateAsync(
        UpdateCollectionArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.UpdateCollectionAsync(
            args.CollectionName,
            format: Format.Json,
            args.ShowBuiltins.ToOption<Anonymous10>(),
            args.Collection,
            cancellationToken);
    }

    public Task DeleteAsync(
        DeleteCollectionArgs args,
        CancellationToken cancellationToken = default) 
    {
        return _client.DeleteCollectionAsync(
            args.CollectionName,
            cancellationToken);   
    }
}