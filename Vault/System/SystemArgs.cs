namespace Vault;

public record DeleteObjectsAndTokensArgs : ReloadCacheArgs
{
    public required Filter Filter { get; init; }

    public bool DryRun { get; init; }
}

public record SetLicenseKeyArgs
{
    public required LicenseKey LicenseKey { get; init; }
}
