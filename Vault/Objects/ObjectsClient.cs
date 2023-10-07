namespace Vault;

public class ObjectsClient : IObjectsClient
{
    private readonly IGeneratedClient _client;

    internal ObjectsClient(IGeneratedClient client)
    {
        _client = client;
    }

    public Task<Count> GetObjectsCountAsync(
        GetObjectsCountArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.GetObjectsCountAsync(
            args.CollectionName,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            cancellationToken);
    }

    public Task<ObjectFieldsPage> ListObjectsAsync(
        ListObjectsArgsBase args,
        CancellationToken cancellationToken = default)
    {
        return _client.ListObjectsAsync(
            args.CollectionName,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.PageSize,
            args.Cursor,
            args.Export,
            args.ExtraTransParam,
            args.ObjectIds,
            ExtractOptions(args).ToAll<ObjectOptions, Anonymous4>(),
            ExtractProps(args),
            cancellationToken);
    }

    public Task<ObjectID> AddObjectAsync(
        AddObjectArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.AddObjectAsync(
            args.CollectionName,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.ExpirationSecs,
            args.Import,
            args.ExportKey,
            args.Object,
            cancellationToken);
    }

    public async Task<ObjectFields> GetObjectByIdAsync(
        GetObjectByIdArgsBase args,
        CancellationToken cancellationToken = default)
    {
        ObjectFields obj = await _client.GetObjectByIdAsync(
            args.CollectionName,
            args.ObjectId,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.ExtraTransParam,
            ExtractOptions(args).ToAll<ObjectOptions, Anonymous14>(),
            ExtractProps(args),
            cancellationToken);
            
        return obj;
    }

    public Task UpdateObjectByIdAsync(
        UpdateObjectByIdArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.UpdateObjectByIdAsync(
            args.CollectionName,
            args.ObjectId,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.ExpirationSecs,
            ExtractArchivedOption(args.Archived).ToAll<ArchivedOption, Anonymous15>(),
            args.Import,
            args.ExportKey,
            args.Object,
            cancellationToken);
    }

    public Task DeleteObjectByIdAsync(
        DeleteObjectByIdArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.DeleteObjectByIdAsync(
            args.CollectionName,
            args.ObjectId,
            ExtractArchivedOption(args.Archived).ToAll<ArchivedOption, Anonymous16>(),
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            cancellationToken);
    }

    public Task<BulkObjectResponse> AddObjectsAsync(
        AddObjectsArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.AddObjectsAsync(
            args.CollectionName,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.ExpirationSecs,
            args.Import,
            args.ExportKey,
            args.Objects,
            cancellationToken);
    }

    public Task<BulkObjectResponse> UpdateObjectsAsync(
        UpdateObjectsArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.UpdateObjectsAsync(
            args.CollectionName,
            args.ExpirationSecs,
            ExtractArchivedOption(args.Archived).ToAll<ArchivedOption, Anonymous19>(),
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.Import,
            args.ExportKey,
            args.Objects,
            cancellationToken);
    }

    public Task DeleteObjectsAsync(
        DeleteObjectsArgs args,
        CancellationToken cancellationToken = default)
    {
        return _client.DeleteObjectsAsync(
            args.CollectionName,
            ExtractArchivedOption(args.Archived).ToAll<ArchivedOption, Anonymous20>(),
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.ObjectIds.Select(id => new ObjectID { Id = id }),
            cancellationToken);
    }

    public Task<ObjectFieldsPage> SearchObjectsAsync(
        SearchObjectsArgsBase args,
        CancellationToken cancellationToken = default)
    {
        return _client.SearchObjectsAsync(
            args.CollectionName,
            args.Reason.OtherMessage,
            args.Reason.Reason.To<Reason>(),
            args.ReloadCache,
            args.PageSize,
            args.Cursor,
            args.ExtraTransParam,
            ExtractOptions(args).ToAll<ObjectOptions, Anonymous21>(),
            ExtractProps(args),
            args.Query,
            cancellationToken);
    }

    private static List<ArchivedOption>? ExtractArchivedOption(bool archived)
    {
        return archived 
            ? new List<ArchivedOption> {ArchivedOption.Archived} 
            : null;
    }

    private static List<ObjectOptions>? ExtractOptions(IObjectOptions args)
    {
        List<ObjectOptions> options = new List<ObjectOptions>();
        
        if (args.Archived)
        {
            options.Add(ObjectOptions.Archived);
        }

        if (args is not IObjectOptionsUnsafe unsafeArgs)
        {
            return args.Archived
                ? options
                : null;
        }
        
        options.Add(ObjectOptions.Unsafe);
        if (unsafeArgs.ShowBuiltins)
        {
            options.Add(ObjectOptions.ShowBuiltins);
        }

        return options;
    }
    
    private static List<string>? ExtractProps(IObjectOptions args)
    {
        if (args is not IObjectOptionsProps propsArgs)
        {
            return null;
        }
        
        var props = propsArgs.Props.ToList();
        if (!props.Any())
        {
            throw new ArgumentException(
                "Props cannot be empty when unsafe is not specified");
        }

        return props;
    }
}