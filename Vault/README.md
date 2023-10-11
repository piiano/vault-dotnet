<p>
  <a href="https://piiano.com/pii-data-privacy-vault/">
    <picture>
      <source media="(prefers-color-scheme: dark)" srcset="https://docs.piiano.com/img/logo-developers-dark.svg">
      <source media="(prefers-color-scheme: light)" srcset="https://docs.piiano.com/img/logo-developers.svg">
      <img alt="Piiano Vault" src="https://docs.piiano.com/img/logo-developers.svg" height="40" />
    </picture>
  </a>
</p>

# Piiano.Vault: a .Net SDK for Vault

vault-dotnet is a client SDK for the Vault written in C# for Microsoft .NET 7.0.
The underlying implementation of the SDK is auto-generated from the Vault OpenApi document.
> **Note:**
> 
> This package is compatible with Vault version 1.8.3.
> For a Vault client compatible with other versions of Vault, check [other versions of this package](https://www.nuget.org/packages/Piiano.Vault#versions-body-tab).

vault-dotnet is a client SDK for Vault written in C# for .Net 7.0.
The underlying implementation of the SDK is auto-generated from the Vault OpenAPI document.

# Contents 

* [Documentation](#documentation)
* [Repository organization](#repository-organization)
  * [The Vault project](#the-vault-project)
  * [The Vault.Tests project](#the-vaulttests-project)
* [Installation](#installation)
* [Using the SDK](#using-the-sdk)
    * [Working with clients](#working-with-clients)
@@ -18,19 +18,19 @@ The underlying implementation of the SDK is auto-generated from the Vault OpenAp
# Documentation
Refer to the API documentation at: https://docs.piiano.com/api/

# Repository organization
The solution has two projects:
- Vault: A library that provides the SDK.
- Vault.Tests: A library containing tests of the SDK.

## The Vault project

The Vault project's OpenAPI document, `openapi.json`, is in its root.

When built, the project generates the file `generated/GeneratedClient.cs` from the OpenAPI document.
The Vault project defines the clients for each of the API resources. Each client is a wrapper over the generated client and provides access to API operations.

## The Vault.Tests project

This project contains tests for each of the clients.
The tests are organized into folders, one folder for each client.
@@ -46,17 +46,18 @@ The tests are organized into folders, one folder for each client.
# Using the SDK

1. Open Vault.sln in your IDE. You can use either Visual Studio or Rider (2023.2.2).
2. Create your project in the solution.
3. In your code:
   -  add `using Vault;`
   - create a client factory using: `var clientFactory = new ClientFactory();`
6. Add the client factory feature you require. For example, (code snippet) to (description of what the code snippet does).
7. Use the `Generated` property to access any other operations defined by the Vault OpenAPI document. For example, (code snippet) to (description of what the code snippet does).

## Working with clients

The client factory provides properties that return clients for Vault resources. Each client is exposed as an interface and provides access to related operations. For example, the `System` property returns a client for the System resource with the `ISystem` interface.   

Here is a list of the clients implemented in this version of the SDK. The interface it implements and a reference to the resource documentation are provided for each client.

| Interface                | Returned by                          | Documentation                                      |
|:-------------------------|:-------------------------------------|:---------------------------------------------------|
@@ -69,11 +70,11 @@ The following table lists the clients that are implemented in this version of th

### Working with a different server

The SDK is configured to work with the Vault server at `http://localhost:8123` by default.
To work with a different server, pass the server URL as the first argument to the constructor of the client factory.

### Working with a different user

By default, the SDK is configured to work with the default token of the Vault Admin user (`pvaultauth`).
To work with a different user, pass the user's token as the second argument to the constructor of the client factory.
For more information on how to get a user's token, see https://docs.piiano.com/guides/manage-users-and-policies/regenerate-user-api-key.
