namespace Vault;

public interface IConfVarClient
{
    Task<string?> Get(
        GetConfVarArgs args,
        CancellationToken cancellationToken = default);

    Task Set(
        SetConfVarArgs args,
        CancellationToken cancellationToken = default);

    Task ClearAll(
        CancellationToken cancellationToken = default);
}