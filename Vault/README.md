# Piiano.Vault: a .Net SDK for Vault

vault-dotnet is a client SDK for the Vault written in C# for Microsoft .NET 7.0.
The underlying implementation of the SDK is auto-generated from the Vault OpenApi document.
> **Note:**
> 
> This package version is 1.1.15 and is compatible with Vault version 1.11.3 .

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
  * [Working with a different server](#working-with-a-different-server)
  * [Working with a different user](#working-with-a-different-user)

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

# Installation

1. Install dot net 7.0 or later: https://docs.microsoft.com/en-us/dotnet/core/install/
2. Install Vault Lite: https://docs.piiano.com/guides/install/
3. Run Vault Lite.
4. Run `dotnet build` in the root of the repository.
5. Run `dotnet test Vault.Tests` in the root of the repository.

# Using the SDK

1. Open Vault.sln in your IDE. You can use either Visual Studio or Rider (2023.2.2).
2. Create your project in the solution.
3. In your code:
   -  add `using Vault;`
   - create a client factory using: `var clientFactory = new ClientFactory();`
   - ClientFactory optional parameters:
      - `uriString` : Vault URL.
      - `userKey` : Bearer token.
      - `defaultRequestHeaders` : Dictionary of HTTP headers.
      - `timeoutValue` : Client timeout.
6. Add the client factory feature you require. For example, (code snippet) to (description of what the code snippet does).
7. Use the `Generated` property to access any other operations defined by the Vault OpenAPI document. For example, (code snippet) to (description of what the code snippet does).

## Working with clients

The client factory provides properties that return clients for Vault resources. Each client is exposed as an interface and provides access to related operations. For example, the `System` property returns a client for the System resource with the `ISystem` interface.   

Here is a list of the clients implemented in this version of the SDK. The interface it implements and a reference to the resource documentation are provided for each client.

| Interface                | Returned by                          | Documentation                                      |
|:-------------------------|:-------------------------------------|:---------------------------------------------------|
| `ISystem`                | `ClientFactory.System`               | https://docs.piiano.com/api/system/                |
| `IConfVar`               | `ClientFactory.ConfVar`              | https://docs.piiano.com/api/config-vars/           |
| `IIAM`                   | `ClientFactory.IAM`                  | https://docs.piiano.com/api/iam/                   |
| `ICollections`           | `ClientFactory.Collections`          | https://docs.piiano.com/api/collections/           |
| `ICollectionProperties`  | `ClientFactory.CollectionProperties` | https://docs.piiano.com/api/collection-properties/ |
| `IObjects`               | `ClientFactory.Objects`              | https://docs.piiano.com/api/objects/               |

### Working with a different server

The SDK is configured to work with the Vault server at `http://localhost:8123` by default.
To work with a different server, pass the server URL as the first argument to the constructor of the client factory.

### Working with a different user

By default, the SDK is configured to work with the default token of the Vault Admin user (`pvaultauth`).
To work with a different user, pass the user's token as the second argument to the constructor of the client factory.
For more information on how to get a user's token, see https://docs.piiano.com/guides/manage-users-and-policies/regenerate-user-api-key.
