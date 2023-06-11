using Microsoft.Extensions.DependencyInjection;
using WebUIAutomationTestFramework.Driver;
using WebUIAutomationTestFramework.Helpers;

namespace WebUIAutomationTestFramework
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<IBrowserDriver, BrowserDriver>()
                .AddScoped<IDriverFixture, DriverFixture>()
                .AddSingleton<ILogger, Logger>();
        }
    }
}
