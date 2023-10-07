namespace Vault;

public record RegenerateUserApiKeyArgs
{
    public required string Name { get; init; }
}

public record SetIamArgs
{
    public required IAMConfig Config { get; init; }
}