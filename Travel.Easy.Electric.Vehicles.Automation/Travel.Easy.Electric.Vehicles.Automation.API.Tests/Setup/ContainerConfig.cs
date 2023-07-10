using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;
using Travel.Easy.Electric.Vehicles.Automation.Common;

namespace Travel.Easy.Electric.Vehicles.Automation.API.Tests.Setup
{
    [Binding]
    public class ContainerConfig
    {
        [ScenarioDependencies]
        public static IServiceCollection ContainerConfiguration()
        {
            var services = new ServiceCollection();

            var assemblyConfigurationAttribute = typeof(ContainerConfig).Assembly
            .GetCustomAttribute<AssemblyConfigurationAttribute>();

            var buildConfigurationName = assemblyConfigurationAttribute?.Configuration;

            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{buildConfigurationName}.json", optional: true, reloadOnChange: true)
             .Build();

            services.AddSingleton<IConfigurationRoot>(configuration);


            services.AddScoped<ScenarioContext>();
            services.AddScoped<RetryPolicy>();
            services.AddScoped<HttpClient>();

            return services;
        }
    }
}
