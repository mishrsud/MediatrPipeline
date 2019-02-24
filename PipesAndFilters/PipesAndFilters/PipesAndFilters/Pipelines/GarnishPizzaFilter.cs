using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PipesAndFilters.Pipelines
{
    public class GarnishPizzaFilter : IPipelineBehavior<PizzaPipelineContext, PipelineResponse>
    {
        private readonly ILogger<GarnishPizzaFilter> _logger;

        public GarnishPizzaFilter(ILogger<GarnishPizzaFilter> logger)
        {
            _logger = logger;
        }
        
        public async Task<PipelineResponse> Handle(PizzaPipelineContext context, CancellationToken cancellationToken, RequestHandlerDelegate<PipelineResponse> next)
        {
            _logger.LogInformation("Adding Garnish");
            context.AddGarnish("Mozarella Cheese, Veggies");

            return await next.Invoke();
        }
    }
}