using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Reflection;
using System;
using WebUIAutomationTestFramework.Settings;
using WebUIAutomationTestFramework.Helpers;

namespace WebUIAutomationTestFramework.Extensions;
public static class WebDriverInitializerExtension
{
    public static IServiceCollection UseWebDriverInitializer(this IServiceCollection services)
    {
        services.AddSingleton(ReadTestSettingsConfig());

        return services;
    }

    private static TestSettings ReadTestSettingsConfig()
    {
        var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var jsonConfigFilePath = Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().Location)
                        + $"/appsettings.{environmentName}.json";
        var jsonFileHelper = new JsonFileHelper(jsonConfigFilePath);
        var testSettingsConfig = jsonFileHelper.GetJsonConfig<TestSettings>();

        return testSettingsConfig;
    }
}
