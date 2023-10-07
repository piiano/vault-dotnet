namespace Vault;

public interface IObjectOptions
{
    bool Archived { get; init; }
}

public interface IObjectOptionsUnsafe : IObjectOptions
{
    bool ShowBuiltins { get; init; }
}

public interface IObjectOptionsProps : IObjectOptions
{
    IEnumerable<string> Props { get; init; }
}

public record AddObjectArgs : ObjectOperationArgs
{
    public required ObjectFields Object { get; init; }

    public string? ExpirationSecs { get; init; }
    public bool Import { get; init; }
    public string? ExportKey { get; init; }
}

public record AddObjectsArgs : ObjectOperationArgs
{
    public required IEnumerable<ObjectFields> Objects { get; init; }

    public string? ExpirationSecs { get; init; }
    public bool Import { get; init; }
    public string? ExportKey { get; init; }
}

public record DeleteObjectByIdArgs : ObjectOperationArgs
{
    public required Guid ObjectId { get; init; }

    public bool Archived { get; init; }
}

public record DeleteObjectsArgs : ObjectOperationArgs
{
    public required IEnumerable<Guid> ObjectIds { get; init; }

    public bool Archived { get; init; }
}

public abstract record GetObjectByIdArgsBase : ObjectOperationArgs, IObjectOptions
{
    public required Guid ObjectId { get; init; }

    public bool Archived { get; init; }
    public string? ExtraTransParam { get; init; }
}

public record GetObjectByIdUnsafeArgs : GetObjectByIdArgsBase, IObjectOptionsUnsafe
{
    public bool ShowBuiltins { get; init; }
}

public record GetObjectByIdPropsArgs : GetObjectByIdArgsBase, IObjectOptionsProps
{
    public required IEnumerable<string> Props { get; init; }
}

public abstract record ListObjectsArgsBase : ObjectOperationArgs, IObjectOptions
{
    public int? PageSize { get; init; }
    public string? Cursor { get; init; }
    public bool Export { get; init; }
    public bool Archived { get; init; }
    public IEnumerable<Guid>? ObjectIds { get; init; }
    public string? ExtraTransParam { get; init; }
}

public record ListObjectsUnsafeArgs : ListObjectsArgsBase, IObjectOptionsUnsafe
{
    public bool ShowBuiltins { get; init; }
}

public record ListObjectsPropsArgs : ListObjectsArgsBase, IObjectOptionsProps
{
    public required IEnumerable<string> Props { get; init; }
}

public record ObjectOperationArgs : ReloadCacheArgs
{
    public required string CollectionName { get; init; }
}

public record GetObjectsCountArgs : ObjectOperationArgs;

public abstract record SearchObjectsArgsBase : ObjectOperationArgs, IObjectOptions
{
    public required Query Query { get; init; }

    public int? PageSize { get; init; }
    public string? Cursor { get; init; }
    public bool Archived { get; init; }
    public string? ExtraTransParam { get; init; }
}

public record SearchObjectsUnsafeArgs : SearchObjectsArgsBase, IObjectOptionsUnsafe
{
    public bool ShowBuiltins { get; init; }
}

public record SearchObjectsPropsArgs : SearchObjectsArgsBase, IObjectOptionsProps
{
    public required IEnumerable<string> Props { get; init; }
}

public record UpdateObjectByIdArgs : ObjectOperationArgs
{
    public required Guid ObjectId { get; init; }
    public required ObjectFields Object { get; init; }
    
    public string? ExpirationSecs { get; init; }
    public bool Archived { get; init; }
    public bool Import { get; init; }
    public string? ExportKey { get; init; }
}

public record UpdateObjectsArgs : AddObjectsArgs
{
    public bool Archived { get; init; }
}