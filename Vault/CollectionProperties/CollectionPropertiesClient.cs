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
            "custom-audit",
            args.ShowBuiltins.ToOption<Anonymous11>(),
            cancellationToken);
    }

    public Task<Property> AddCollectionPropertyAsync(
        AddCollectionPropertyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.AddCollectionPropertyAsync(
            args.CollectionName,
            "custom-audit",
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
            "custom-audit",
            args.PropertyName,
            cancellationToken);
    }

    public Task<Property> UpdateCollectionPropertyAsync(
        UpdateCollectionPropertyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.UpdateCollectionPropertyAsync(
            args.CollectionName,
            "custom-audit",
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
            "custom-audit",
            args.PropertyName,
            cancellationToken);
    }
}