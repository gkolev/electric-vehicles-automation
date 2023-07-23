using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolidToken.SpecFlow.DependencyInjection;
using TechTalk.SpecFlow;
using Travel.Easy.Electric.Vehicles.Automation.Common;
using Travel.Easy.Electric.Vehicles.Automation.Facades;
using TravelEasy.EV.DataLayer;

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
            services.AddScoped<CarsFacade>();
            services.AddScoped<UsersFacade>();

            services.AddDbContext<ElectricVehiclesContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DbConnection"));

            });
            return services;
        }
    }
}
