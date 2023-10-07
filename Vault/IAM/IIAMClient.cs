namespace Vault;

public interface IIAMClient
{
    Task<IAMConfig> GetIAM(
        CancellationToken cancellationToken = default);

    Task SetIAM(
        SetIamArgs args,
        CancellationToken cancellationToken = default);

    Task<APIKey> RegenerateUserApiKeyAsync(
        RegenerateUserApiKeyArgs args,
        CancellationToken cancellationToken = default);
}