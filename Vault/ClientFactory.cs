using System.Net.Http.Headers;
using System.Collections.Generic;
using System;
namespace Vault;

public class ClientFactory
{
    private const string DefaultUriString = "http://localhost:8123";
    private const string InitialAdminUserKey = "pvaultauth";
//    private const Dictionary<string, string> DefaultHeaders = new Dictionary<string, string>(){};
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
        string userKey = InitialAdminUserKey,
        Dictionary<string, string>? defaultRequestHeaders = null,
        TimeSpan? timeoutValue = null)
    {
        Console.WriteLine("jaier");
        var httpClient = new HttpClient(new HttpInterceptor())
        {
            BaseAddress = new Uri(uriString),
            DefaultRequestHeaders =
            {
                Authorization = AuthenticationHeaderValue.Parse($"Bearer {userKey}")
            },
        };

        if (defaultRequestHeaders?.Count > 0)
        {
            foreach (var header in defaultRequestHeaders)
            {
                httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        if (timeoutValue.HasValue)
        {
            httpClient.Timeout = timeoutValue.Value;
        }

        GeneratedClient = new GeneratedClient(httpClient);
        Collections = new CollectionsClient(GeneratedClient);
        Objects = new ObjectsClient(GeneratedClient);
        System = new SystemClient(GeneratedClient);
        ConfVar = new ConfVarClient(GeneratedClient);
        IAM = new IAMClient(GeneratedClient);
        CollectionProperties = new CollectionPropertiesClient(GeneratedClient);
    }
}