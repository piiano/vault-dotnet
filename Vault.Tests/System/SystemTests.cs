﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.Generic;

namespace Vault.Tests;

public class SystemTests
{
    private ISystemClient _client = null!;
    private const string UpStatus = "pass";
        
    [SetUp]
    public void Setup()
    {
        _client = new ClientFactory().System;
    }

    [Test]
    public async Task TestDataHealth()
    {
        Health health = await _client.GetDataServiceStatusAsync();
            
        Assert.AreEqual(UpStatus, health.Status);
    }

    [Test]
    public async Task TestControlHealth()
    {
        Health health = await _client.GetControlServiceStatusAsync();
            
        Assert.AreEqual(UpStatus, health.Status);
    }
        
    [Test]
    public async Task TestClusterInformation()
    {
        var info = await _client.GetClusterInformationAsync();

        Assert.NotNull(info);
    }
        
    [Test]
    public async Task TestGetConfiguration()
    {
        var config = await _client.GetConfigurationAsync();

        Assert.NotNull(config);
    }
        
    [Test]
    public async Task TestLicenseKey()
    {
        string key = Environment.GetEnvironmentVariable("PVAULT_SERVICE_LICENSE")!;
        if (String.IsNullOrEmpty(key))
        {
            return;
        }

        await _client.SetLicenseAsync(
            new SetLicenseKeyArgs
            {
                LicenseKey = new LicenseKey
                {
                    Key = key
                }
            });
            
        var license = await _client.GetLicenseAsync();

        Assert.NotNull(license);
        Assert.AreEqual(key, license.Key);
    }

    [Test]
    public async Task TestGetVaultVersion()
    {
        ProductVersion version = await _client.GetVaultVersionAsync();
            
        Assert.IsNotEmpty(version.Vault_id);
        Assert.IsNotEmpty(version.Vault_version);
    }

    [Test]
    public async Task TestGetExportKey()
    {
        var response = await _client.GetExportKeyAsync();

        Assert.NotNull(response);
    }

    [Test]
    public async Task TestGetKmsStatus()
    {
        var kms = await _client.GetKmsStatusAsync();

        Assert.NotNull(kms);
    }

    [Test]
    public async Task TestAddHeaders()
    {
        // Init client with the headers
        var client = new ClientFactory(defaultRequestHeaders: new Dictionary<string, string> { { "X-Pvault-Request-ID", "12345" } }).System;
        var config = await client.GetConfigurationAsync();
        Assert.NotNull(config);
    }
}
