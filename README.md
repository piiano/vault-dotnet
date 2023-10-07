# Piiano.Vault: A .Net SDK for Vault

vault-dotnet is a client SDK for the Vault written in C# for .Net 7.0.
The underlying implementation of the SDK is auto-generated from the Vault OpenApi document.

# Contents 

* [Documentation](#documentation)
* [Repository Organization](#repository-organization)
  * [The Vault Project](#the-vault-project)
  * [The Vault.Tests Project](#the-vaulttests-project)
* [Installation](#installation)
* [Using the SDK](#using-the-sdk)
    * [Working with clients](#working-with-clients)
  * [Working with a different server](#working-with-a-different-server)
  * [Working with a different user](#working-with-a-different-user)

# Documentation
Refer to the API documentation at: https://docs.piiano.com/api/

# Repository Organization
The solution has two projects:
- Vault: A library that provides the SDK.
- Vault.Tests: A library contains tests of the SDK.

## The Vault Project

The Vault project contains the OpenApi specification in its root: `openapi.json`.

When built, the project generates the file generated/GeneratedClient.cs from the Open API specification.
The project defines the clients for each of the endpoint clients. Each one is a wrapper over the generated client and provides access to a group of endpoints.

## The Vault.Tests Project

This project contains tests for each of the clients.
The tests are organized into folders, one folder for each client.

# Installation

1. Install dot net 7.0 or later: https://docs.microsoft.com/en-us/dotnet/core/install/
2. Install Vault Lite: https://docs.piiano.com/guides/install/
3. Run Vault Lite.
4. Run `dotnet build` in the root of the repository.
5. Run `dotnet test` in the root of the repository.

# Using the SDK

1. Open Vault.sln in your IDE. You can use either Visual Studio or Rider (2023.2.2).
2. Create your own project in the solution and add a reference to Vault.
3. In your code add `using Vault;`
4. In your code, create a client factory using: `var clientFactory = new ClientFactory();`
5. The client factory provides properties that return clients for groups of Vault endpoints. Each client is exposed through an interface and wraps the automatically generated client.
6. In addition, the client factory provides a property called `Generated` that returns the underlying generated client. This client provides access to all methods defined by the Open API specification, though in a less convenient form.

## Working with clients

The client factory provides properties that return clients for Vault endpoints. Each client is exposed as an interface and provides access to a group of related endpoints. For example, the `System` property returns a client for the System endpoints with the `ISystem` interface.   

The following table lists the clients that are implemented in this version of the SDK. For each client, it lists the interface that it implements and a reference to the documentation for the endpoints that it provides access to.

| Interface                | Returned by                          | Documentation                                      |
|:-------------------------|:-------------------------------------|:---------------------------------------------------|
| `ISystem`                | `ClientFactory.System`               | https://docs.piiano.com/api/system/                |
| `IConfVar`               | `ClientFactory.ConfVar`              | https://docs.piiano.com/api/config-vars/           |
| `IIAM`                   | `ClientFactory.IAM`                  | https://docs.piiano.com/api/iam/                   |
| `ICollections`           | `ClientFactory.Collections`          | https://docs.piiano.com/api/collections/           |
| `ICollectionProperties`  | `ClientFactory.CollectionProperties` | https://docs.piiano.com/api/collection-properties/ |
| `IObjects`               | `ClientFactory.Objects`              | https://docs.piiano.com/api/objects/               |

### Working with a different server

By default, the SDK is configured to work with the Vault server at `http://localhost:8123`.
To work with a different server, pass the server URL as the first argument to the constructor of the client factory.

### Working with a different user

By default, the SDK is configured to work with the default token of the Admin user of Vault (`pvaultauth`).
To work with a different user, pass the user's token as the second argument to the constructor of the client factory.
For more information on how to get a user's token, see https://docs.piiano.com/guides/manage-users-and-policies/regenerate-user-api-key
