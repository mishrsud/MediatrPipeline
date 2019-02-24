using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PipesAndFilters.Pipelines;

namespace PipesAndFilters
{
    public class Startup : IStartup
    {
        public void ConfigureServices(
            HostBuilderContext hostBuilderContext, 
            IServiceCollection services)
        {
            services.AddMediatR();
            services.AddHostedService<EntryPointHostedService>();
            services.AddLogging(builder => builder.AddConsole());
            ConfigurePipeline(services);
        }

        public void SetupAppConfiguration(
            HostBuilderContext hostBuilderContext, 
            IConfigurationBuilder configurationBuilder,
            string[] commandLineArgs)
        {
            
        }

        private void ConfigurePipeline(IServiceCollection services)
        {
            // NOTE: Services can be added using a ServiceDescriptor:
            //  services.Add(
            //                  new ServiceDescriptor(
            //                    typeof(IPipelineBehavior<,>), 
            //                    typeof(LoggingFilter<,>), ServiceLifetime.Scoped));
            
            // NOTE: Attempt at making the creation of pipeline more intuitive
            //services.AddPipeFilter<ValidationFilter, PiplineRequest, StepOneResponse>(ServiceLifetime.Scoped);
            
            services.AddScoped(typeof(IPipelineBehavior<,>),
                typeof(LoggingFilter<,>));
            services.AddScoped(typeof(IPipelineBehavior<PizzaPipelineContext, PipelineResponse>),
                typeof(ExceptionHandlingFilter));
            services.AddScoped(
                typeof(IPipelineBehavior<PizzaPipelineContext, PipelineResponse>),
                typeof(ValidationFilter));
            
            services.AddScoped(
                typeof(IPipelineBehavior<PizzaPipelineContext, PipelineResponse>), 
                typeof(MakePizzaCrustFilter));
            services.AddScoped(
                typeof(IPipelineBehavior<PizzaPipelineContext, PipelineResponse>),
                typeof(GarnishPizzaFilter));
        }
    }
}