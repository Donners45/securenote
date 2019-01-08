# api.boilerplate
Lightweight .NET Core WebAPI boilerplate. Contains three projects, API, unit tests and integration tests.

# API
ASP.NET Core WebAPI 2.1 project.

Comes configured with Swagger UI at root url.

# Unit Tests
Basic XUnit project.

# Integration Tests
XUnit project with test host.

Contains a `TestFixture<TStartup>` class for use with `Xunit.IClassFixture<TFixture>`.

To use create a new unit test which implements `IClassFixture<TFixture>` providing `TestFixture<TStartup>` this will create a default server and client. 
