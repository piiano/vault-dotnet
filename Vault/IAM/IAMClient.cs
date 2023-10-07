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
            cancellationToken);
    }

    public async Task SetIAM(
        SetIamArgs args,
        CancellationToken cancellationToken = default)
    {
        await _client.SetIamConfAsync(
            args.Config,
            cancellationToken);
    }

    public Task<APIKey> RegenerateUserApiKeyAsync(
        RegenerateUserApiKeyArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.RegenerateUserApiKeyAsync(
            new UserName { Name = args.Name },
            cancellationToken);
    }
}