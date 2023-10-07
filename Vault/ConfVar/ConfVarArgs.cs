namespace Vault;

public record GetConfVarArgs
{
    public required string Name { get; init; }
}

public record SetConfVarArgs : GetConfVarArgs
{
    public required string Value { get; init; }
}