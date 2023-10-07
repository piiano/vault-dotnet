namespace Vault;

public record ListCollectionPropertiesArgs : ListCollectionsArgs
{
    public required string CollectionName { get; init; }
}

public record GetCollectionPropertyArgs
{
    public required string CollectionName { get; init; }
    
    public required string PropertyName { get; init; }
}

public record DeleteCollectionPropertyArgs
{
    public required string CollectionName { get; init; }
    
    public required string PropertyName { get; init; }
}

public record UpdateCollectionPropertyArgs : GetCollectionPropertyArgs
{
    public required UpdatePropertyRequest Property { get; init; }
}

public record AddCollectionPropertyArgs : GetCollectionPropertyArgs
{
    public required Property Property { get; init; }
}