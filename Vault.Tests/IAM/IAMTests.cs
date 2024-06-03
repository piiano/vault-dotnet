using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Vault.Tests;

public class IAMTests
{
    private IIAMClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _client = new ClientFactory().IAM;
    }

    [Test]
    public async Task TestIAM()
    {
        var toml1 = await _client.GetIAM();

        await _client.SetIAM(new SetIamArgs
        {
            Config = toml1
        });

        var toml2 = await _client.GetIAM();

        var json1 = JsonConvert.SerializeObject(toml1);
        var json2 = JsonConvert.SerializeObject(toml2);

        Assert.AreEqual(json1, json2);
    }

    [Test]
    public async Task TestRegenerateUserApiKey()
    {
        var iam = await _client.GetIAM();
        Assert.NotNull(iam);

        try
        {
            User user = iam.Users["VaultAdmin"];

        }
        catch (KeyNotFoundException)
        {
            System.Console.WriteLine("User VaultAdmin not found. Skipping IAM test");
            return;
        }

        var apiKey = await _client.RegenerateUserApiKeyAsync(
            new RegenerateUserApiKeyArgs
            {
                Name = "VaultAdmin"
            });

        string? keyString = apiKey.Api_key;
        Assert.IsNotEmpty(keyString);

        IIAMClient apiKeyIamClient = new ClientFactory(userKey: keyString!).IAM;
        iam = await apiKeyIamClient.GetIAM();
        Assert.NotNull(iam);

        var newApiKey = await _client.RegenerateUserApiKeyAsync(
            new RegenerateUserApiKeyArgs
            {
                Name = "VaultAdmin"
            });

        string? newKeyString = newApiKey.Api_key;
        Assert.IsNotEmpty(newKeyString);
        Assert.AreNotEqual(keyString, newKeyString);

        var exception = Assert.ThrowsAsync<ApiException<HTTPError>>(
            () => apiKeyIamClient.GetIAM());
        Assert.IsNotNull(exception);
        Assert.AreEqual(401, exception!.StatusCode);
    }
}