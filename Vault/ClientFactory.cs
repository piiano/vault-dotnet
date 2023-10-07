using System.Net.Http.Headers;

namespace Vault;

public class ClientFactory
{
    private const string DefaultUriString = "http://localhost:8123";
    private const string InitialAdminUserKey = "pvaultauth";
    
    public ICollectionsClient Collections { get; }

    public IObjectsClient Objects { get; }

    public ISystemClient System { get; }

    public IConfVarClient ConfVar { get; }
        
    public IIAMClient IAM { get; }
        
    public ICollectionPropertiesClient CollectionProperties { get; }

    // This property gives access to the underlying generated client for APIs that have not been wrapped.
    public IGeneratedClient GeneratedClient { get; }
    
    public ClientFactory(
        string uriString = DefaultUriString,
        string userKey = InitialAdminUserKey)
    {
        var httpClient = new HttpClient(new HttpInterceptor())
        {
            BaseAddress = new Uri(uriString),
            DefaultRequestHeaders =
            {
                Authorization = AuthenticationHeaderValue.Parse($"Bearer {userKey}")
            }
        };
        
        GeneratedClient = new GeneratedClient(httpClient);
        Collections = new CollectionsClient(GeneratedClient);
        Objects = new ObjectsClient(GeneratedClient);
        System = new SystemClient(GeneratedClient);
        ConfVar = new ConfVarClient(GeneratedClient);
        IAM = new IAMClient(GeneratedClient);
        CollectionProperties = new CollectionPropertiesClient(GeneratedClient);
    }
}