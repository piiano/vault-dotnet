namespace Vault;

internal class ConfVarClient : IConfVarClient
{
    private readonly IGeneratedClient _client;

    internal ConfVarClient(IGeneratedClient client)
    {
        _client = client;
    }

    public async Task<string?> Get(
        GetConfVarArgs args,
        CancellationToken cancellationToken = default)
    {
        ConfVar variable = await _client.GetConfVarAsync(
            args.Name,
            cancellationToken);

        return variable.Value.ToString();
    }
        
    public Task Set(
        SetConfVarArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.SetConfVarAsync(
            args.Name,
            new ConfVarValue
            {
                Value = args.Value
            },
            cancellationToken);
    }

    public Task ClearAll(
        CancellationToken cancellationToken = default)
    {
        return _client.ClearAllConfVarsAsync(
            cancellationToken);
    }
}