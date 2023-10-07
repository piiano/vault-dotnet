namespace Vault;

public record ReloadCacheArgs : ReasonArgs
{
    public bool ReloadCache { get; init; }
}