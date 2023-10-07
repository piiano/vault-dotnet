namespace Vault;

public record ListCollectionsArgs
{
    public bool ShowBuiltins { get; init; }
}

public record GetCollectionArgs : ListCollectionsArgs
{
    public required string CollectionName { get; init; }
}

public record DeleteCollectionArgs : ListCollectionsArgs
{
    public required string CollectionName { get; init; }
}

public record AddCollectionArgs : ListCollectionsArgs
{
    public required Collection Collection { get; init; }
}

public record UpdateCollectionArgs : ListCollectionsArgs
{
    public required Collection Collection { get; init; }
    public required string CollectionName { get; init; }
}