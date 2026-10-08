using Microsoft.Extensions.DependencyInjection;
using ApiAutomationFramework.Tests.Clients;

namespace ApiAutomationFramework.Tests.Helpers;

public static class DependencyInjection
{
    public static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        services.AddHttpClient("HttpBin", client =>
        {
            client.BaseAddress = new Uri(ConfigurationHelper.BaseUrl);
        });

        services.AddTransient<ApiRequestHelper>();
        services.AddTransient<HttpBinClient>();

        return services.BuildServiceProvider();
    }
}