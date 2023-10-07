namespace Vault;

public class SystemClient : ISystemClient
{
    private readonly IGeneratedClient _client;

    internal SystemClient(IGeneratedClient client)
    {
        _client = client;
    }
        
    public Task<Health> GetDataServiceStatusAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.DataHealthAsync(cancellationToken);
    }
        
    public Task<Health> GetControlServiceStatusAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.ControlHealthAsync(cancellationToken);
    }
        
    public Task<AllGenerations> GetClusterInformationAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetClusterInfoAsync(cancellationToken);
    }
    
    public Task<IEnumerable<DeletionCount>> DeleteObjectsAndTokensAsync(
        DeleteObjectsAndTokensArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.GarbageCollectionAsync(
            args.Filter,
            args.DryRun,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            cancellationToken);
    }
        
    public Task<Config> GetConfigurationAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetConfigurationAsync(cancellationToken);
    }
        
    public Task<License> GetLicenseAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetLicenseAsync(cancellationToken);
    }
        
    public Task SetLicenseAsync(
        SetLicenseKeyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.SetLicenseAsync(args.LicenseKey, cancellationToken);
    }
        
    public Task<ProductVersion> GetVaultVersionAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetVaultVersionAsync(cancellationToken);
    }

    public Task RotateKeysAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.RotateKeysAsync(cancellationToken);
    }
        
    public Task<ExportKeyResponse> GetExportKeyAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetExportKeyAsync(cancellationToken);
    }

    public Task<KMSStatusResponse> GetKmsStatusAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetKmsAsync(cancellationToken);
    }
}