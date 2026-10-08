using Microsoft.Extensions.Configuration;

namespace ApiAutomationFramework.Tests.Helpers;

public static class ConfigurationHelper
{
    private static readonly IConfiguration Configuration =
        new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

    public static string BaseUrl =>
        Configuration["ApiSettings:BaseUrl"]
        ?? throw new InvalidOperationException(
            "BaseUrl is not configured.");
}