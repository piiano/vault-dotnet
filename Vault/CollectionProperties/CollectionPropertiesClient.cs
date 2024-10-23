namespace Vault;

internal class CollectionPropertiesClient : ICollectionPropertiesClient
{
    private readonly IGeneratedClient _client;

    internal CollectionPropertiesClient(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<IEnumerable<Property>> ListCollectionPropertiesAsync(
        ListCollectionPropertiesArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.ListCollectionPropertiesAsync(
            args.CollectionName,
            args.ShowBuiltins.ToOption<Anonymous11>(),
            cancellationToken);
    }

    public Task<Property> AddCollectionPropertyAsync(
        AddCollectionPropertyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.AddCollectionPropertyAsync(
            args.CollectionName,
            args.PropertyName,
            args.Property,
            cancellationToken);
    }

    public Task<Property> GetCollectionPropertyAsync(
        GetCollectionPropertyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.GetCollectionPropertyAsync(
            args.CollectionName,
            args.PropertyName,
            cancellationToken);
    }

    public Task<Property> UpdateCollectionPropertyAsync(
        UpdateCollectionPropertyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.UpdateCollectionPropertyAsync(
            args.CollectionName, 
            args.PropertyName, 
            args.Property, 
            cancellationToken);
    }

    public Task DeleteCollectionPropertyAsync(
        DeleteCollectionPropertyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.DeleteCollectionPropertyAsync(
            args.CollectionName, 
            args.PropertyName, 
            cancellationToken);
    }
}