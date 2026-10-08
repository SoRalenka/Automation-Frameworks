using ApiAutomationFramework.Tests.Clients;
using ApiAutomationFramework.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace ApiAutomationFramework.Tests.Fixtures;

public class ApiTestFixture : IDisposable
{
    private readonly ServiceProvider _serviceProvider;

    public HttpBinClient HttpBinClient { get; }

    public ApiTestFixture()
    {
        _serviceProvider = DependencyInjection.ConfigureServices();

        HttpBinClient =
            _serviceProvider.GetRequiredService<HttpBinClient>();
    }

    public void Dispose()
    {
        _serviceProvider.Dispose();
    }
}