namespace Vault;

public class IAMClient : IIAMClient
{
    private readonly IGeneratedClient _client;

    internal IAMClient(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<IAMConfig> GetIAM(
        CancellationToken cancellationToken = default)
    {
        return _client.GetIamConfAsync(
            "custom-audit",
            cancellationToken);
    }

    public async Task SetIAM(
        SetIamArgs args,
        CancellationToken cancellationToken = default)
    {
        await _client.SetIamConfAsync(
            "custom-audit",
            args.Config,
            cancellationToken);
    }

    public Task<APIKey> RegenerateUserApiKeyAsync(
        RegenerateUserApiKeyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.RegenerateUserApiKeyAsync(
            "custom-audit",
            new UserName { Name = args.Name },
            cancellationToken);
    }
}