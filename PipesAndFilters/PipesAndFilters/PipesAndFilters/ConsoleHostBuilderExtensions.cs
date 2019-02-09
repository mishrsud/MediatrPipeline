using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PipesAndFilters
{
    public interface IStartup
    {
        void ConfigureServices(
            HostBuilderContext hostBuilderContext, 
            IServiceCollection services);

        void SetupAppConfiguration(
            HostBuilderContext hostBuilderContext, 
            IConfigurationBuilder configurationBuilder,
            string[] commandLineArgs);
    }
    
    public static class ConsoleHostBuilderExtensions
    {
        public static IHostBuilder UseStartup<TStartup>(this IHostBuilder hostBuilder, TStartup startup, string[] commandLineArgs)
            where TStartup : IStartup
        {
            hostBuilder.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
                startup.SetupAppConfiguration(hostBuilderContext, configurationBuilder, commandLineArgs));

            hostBuilder.ConfigureServices(startup.ConfigureServices);
            
            return hostBuilder;
        }
    }
}