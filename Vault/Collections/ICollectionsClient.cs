namespace Vault;

public interface ICollectionsClient
{
    Task<IEnumerable<Collection>> ListAsync(
        CancellationToken cancellationToken = default);
        
    Task<IEnumerable<Collection>> ListAsync(
        ListCollectionsArgs args,
        CancellationToken cancellationToken = default);

    Task<Collection> AddAsync(
        AddCollectionArgs args,
        CancellationToken cancellationToken = default);

    Task<Collection> GetAsync(
        GetCollectionArgs args,
        CancellationToken cancellationToken = default);

    Task<Collection> UpdateAsync(
        UpdateCollectionArgs args,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        DeleteCollectionArgs args,
        CancellationToken cancellationToken = default);
}