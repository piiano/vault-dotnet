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
        return _client.GetClusterInfoAsync("custom-audit", cancellationToken);
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
            "custom-audit",
            args.ReloadCache,
            cancellationToken);
    }

    public Task<Config> GetConfigurationAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetConfigurationAsync("custom-audit", cancellationToken);
    }

    public Task<License> GetLicenseAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetLicenseAsync("custom-audit", cancellationToken);
    }

    public Task SetLicenseAsync(
        SetLicenseKeyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.SetLicenseAsync("custom-audit", args.LicenseKey, cancellationToken);
    }

    public Task<ProductVersion> GetVaultVersionAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetVaultVersionAsync("custom-audit", cancellationToken);
    }

    public Task RotateKeysAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.RotateKeysAsync("custom-audit", cancellationToken);
    }

    public Task<ExportKeyResponse> GetExportKeyAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetExportKeyAsync("custom-audit", cancellationToken);
    }

    public Task<KMSStatusResponse> GetKmsStatusAsync(
        CancellationToken cancellationToken = default)
    {
        return _client.GetKmsAsync("custom-audit", cancellationToken);
    }
}