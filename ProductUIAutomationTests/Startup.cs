using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.Extensions.DependencyInjection;
using ProductAPI.Data;
using ProductAPI.Repository;
using ProductUIAutomationTests.Pages;
using WebUIAutomationTestFramework.Driver;
using WebUIAutomationTestFramework.Extensions;
using WebUIAutomationTestFramework.Helpers;

namespace ProductUIAutomationTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(projectPath)
                    .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ProductDbContext>(
                                options => options
                                          .UseSqlServer(connectionString));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.UseWebDriverInitializer();
            services.AddScoped<IBrowserDriver, BrowserDriver>();
            services.AddScoped<IDriverFixture, DriverFixture>();
            services.AddScoped<IShared, Shared>();
            services.AddScoped<IProductPage, ProductPage>();
            services.AddSingleton<ILogger, Logger>();
        }
    }
}
