namespace Vault;

public interface ISystemClient
{
    Task<Health> GetDataServiceStatusAsync(
        CancellationToken cancellationToken = default);
    
    Task<Health> GetControlServiceStatusAsync(
        CancellationToken cancellationToken = default);
    
    Task<AllGenerations> GetClusterInformationAsync(
        CancellationToken cancellationToken = default);

    Task<IEnumerable<DeletionCount>> DeleteObjectsAndTokensAsync(
        DeleteObjectsAndTokensArgs args,
        CancellationToken cancellationToken = default);
    
    Task<Config> GetConfigurationAsync(
        CancellationToken cancellationToken = default);
    
    Task<License> GetLicenseAsync(
        CancellationToken cancellationToken = default);
    
    Task SetLicenseAsync(
        SetLicenseKeyArgs args,
        CancellationToken cancellationToken = default);

    Task<ProductVersion> GetVaultVersionAsync(
        CancellationToken cancellationToken = default);

    Task RotateKeysAsync(
        CancellationToken cancellationToken = default);

    Task<ExportKeyResponse> GetExportKeyAsync(
        CancellationToken cancellationToken = default);
    
    Task<KMSStatusResponse> GetKmsStatusAsync(
        CancellationToken cancellationToken = default);
}