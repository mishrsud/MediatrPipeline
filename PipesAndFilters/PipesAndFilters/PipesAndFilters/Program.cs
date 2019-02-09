using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PipesAndFilters
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Entry point, called by runtime")]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .UseStartup(new Startup(), args);
            
            await host.RunConsoleAsync();
        }
    }
}