using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using Newtonsoft.Json;
using Tomlyn;
using Tomlyn.Model;

namespace Vault;

// HttpInterceptor intercepts the HTTP requests in order to handle two problems:
// 1. A problem in the generated client
//    The generated client treats the toml content type as JSON.
//    - For requests that should contain toml, the generated client serializes to JSON.
//    The interceptor then converts it to toml (by deserializing the JSON and converting the model to toml).
//    - For responses containing toml, the interceptor deserializes the toml to the model and then serializes
//    to JSON. This is then accepted by the generated client which deserializes it as JSON.
// 2. A problem in the open api spec:
//    Fields in the Configuration model that specify a duration are mistakenly
//    specified in the Open API as integers, but are actually strings (e.g. "1h2s").
//    The HttpInterceptor converts these strings to an integer representing the number of seconds
//    indicated by the duration string.
internal class HttpInterceptor : DelegatingHandler
{
    private const string IamConfPath = "ctl/iam/conf";
    private const string SystemConfigurationPath = "system/info/configuration";
    private const string MediaTypeToml = "application/toml";

    private static readonly Dictionary<string, Type> EndpointEntitiesTypes = new()
    {
        {IamConfPath, typeof(IAMConfig)},
        {SystemConfigurationPath, typeof(Config)},
    };

    private static readonly string[] ConfigDurationFields =
    {
        "db.gc.retention_period",
        "db.migration.initial_wait_between_retries",
        "db.migration.max_wait_between_retries",
        "expiration.tokens",
        "expiration.associated_objects",
        "expiration.unassociated_objects",
        "service.cache_refresh_interval",
        "service.archive_prune_interval",
        "service.stats_interval",
        "service.os_stats_interval",
        "service.config_report_interval",
    };
    
    public HttpInterceptor()
        : base(new HttpClientHandler())
    {
    }
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, 
        CancellationToken cancellationToken)
    {
        if (request.Content?.Headers.ContentType?.MediaType == MediaTypeToml)
        {
            await JsonToToml(request, cancellationToken);
        }
        
        Trace.WriteLine(request);
        HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
        
        if (response.Content.Headers.ContentType?.MediaType == MediaTypeToml)
        {
            return await TomlToJson(request, response, cancellationToken);
        }
        
        Trace.WriteLine(response);
        return response;
    }

    private static async Task JsonToToml(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string jsonContent = await request.Content!.ReadAsStringAsync(cancellationToken);
        Type type = EndpointEntitiesTypes
            .FirstOrDefault(
                kv => IsRequestToPath(request, kv.Key))
            .Value;
        
        object jsonObject = JsonConvert.DeserializeObject(
            jsonContent,
            type)!;

        string tomlContent = Toml.FromModel(jsonObject);

        request.Content = new StringContent(tomlContent, Encoding.UTF8, MediaTypeToml);
    }
    
    private static async Task<HttpResponseMessage> TomlToJson(
        HttpRequestMessage request, 
        HttpResponseMessage response, 
        CancellationToken cancellationToken)
    {
        string tomlContent = await response.Content.ReadAsStringAsync(cancellationToken);
        TomlTable toml = Toml.ToModel(tomlContent);

        if (IsRequestToPath(request, SystemConfigurationPath))
        {
            foreach (string field in ConfigDurationFields)
            {
                DurationParser.ReplaceDurationStringWithSecondsInteger(toml, field);
            }
        }

        string jsonContent = JsonConvert.SerializeObject(toml);

        return new HttpResponseMessage
        {
            StatusCode = response.StatusCode,
            Content = new StringContent(jsonContent, Encoding.UTF8, MediaTypeNames.Application.Json),
            RequestMessage = response.RequestMessage
        };
    }
    
    private static bool IsRequestToPath(HttpRequestMessage request, string path)
    {
        return request.RequestUri?.ToString().Contains(path) ?? false;
    }
}