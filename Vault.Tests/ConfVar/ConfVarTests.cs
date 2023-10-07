using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Vault.Tests.ConfVar;

[TestFixture]
public class ConfVarTests
{
    private IConfVarClient _client = null!;

    [SetUp]
    public void Setup()
    {
        _client = new ClientFactory().ConfVar;
    }

    [Test]
    public async Task Test()
    {
        const string key = "log_level";
        const string value = "info";

        await _client.ClearAll();
        
        await _client.Set(new SetConfVarArgs
        {
            Name = key,
            Value = value
        });
        
        string? newVal = await _client.Get(new GetConfVarArgs
        {
            Name = key
        });
        
        Assert.AreEqual(value, newVal);
    }
}