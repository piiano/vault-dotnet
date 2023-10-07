namespace Vault;

public interface ICollectionPropertiesClient
{
    Task<IEnumerable<Property>> ListCollectionPropertiesAsync(
        ListCollectionPropertiesArgs args,
        CancellationToken cancellationToken = default);
    
    Task<Property> AddCollectionPropertyAsync(
        AddCollectionPropertyArgs args,
        CancellationToken cancellationToken = default);
    
    Task<Property> GetCollectionPropertyAsync(
        GetCollectionPropertyArgs args,
        CancellationToken cancellationToken = default);
    
    Task<Property> UpdateCollectionPropertyAsync(
        UpdateCollectionPropertyArgs args,
        CancellationToken cancellationToken = default);
    
    Task DeleteCollectionPropertyAsync(
        DeleteCollectionPropertyArgs args,
        CancellationToken cancellationToken = default);
}