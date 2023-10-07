namespace Vault;

public interface IObjectsClient
{
    Task<Count> GetObjectsCountAsync(
        GetObjectsCountArgs args,
        CancellationToken cancellationToken = default);
        
    Task<ObjectFieldsPage> ListObjectsAsync(
        ListObjectsArgsBase args,
        CancellationToken cancellationToken = default);
        
    Task<ObjectID> AddObjectAsync(
        AddObjectArgs args,
        CancellationToken cancellationToken = default);
        
    Task<ObjectFields> GetObjectByIdAsync(
        GetObjectByIdArgsBase args,
        CancellationToken cancellationToken = default);
        
    Task UpdateObjectByIdAsync(
        UpdateObjectByIdArgs args,
        CancellationToken cancellationToken = default);
        
    Task DeleteObjectByIdAsync(
        DeleteObjectByIdArgs args,
        CancellationToken cancellationToken = default);
        
    Task<BulkObjectResponse> AddObjectsAsync(
        AddObjectsArgs args,
        CancellationToken cancellationToken = default);
        
    Task<BulkObjectResponse> UpdateObjectsAsync(
        UpdateObjectsArgs args,
        CancellationToken cancellationToken = default);
        
    Task DeleteObjectsAsync(
        DeleteObjectsArgs args,
        CancellationToken cancellationToken = default);
        
    Task<ObjectFieldsPage> SearchObjectsAsync(
        SearchObjectsArgsBase args,
        CancellationToken cancellationToken = default);
}