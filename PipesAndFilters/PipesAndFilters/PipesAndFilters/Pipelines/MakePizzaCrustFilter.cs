using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PipesAndFilters.Pipelines
{
    public class MakePizzaCrustFilter : IPipelineBehavior<PizzaPipelineContext, PipelineResponse>
    {
        private readonly ILogger<MakePizzaCrustFilter> _logger;
        private static int _fakeCounter = 0;

        public MakePizzaCrustFilter(ILogger<MakePizzaCrustFilter> logger)
        {
            _logger = logger;
        }
        
        public async Task<PipelineResponse> Handle(PizzaPipelineContext context, CancellationToken cancellationToken, RequestHandlerDelegate<PipelineResponse> nextFilter)
        {
            context.CurrentStep = nameof(MakePizzaCrustFilter);

            if (_fakeCounter % 4 == 0)
            {
                _fakeCounter++;
                throw new Exception("Boom");
            }
            
            if ( context.Request.Values.Contains("continue"))
            {
                _logger.LogInformation("Setting pizza base");
                context.SetPizzaBase(PizzaBaseFlourType.Wheat, PizzaBaseType.Pan);
                _fakeCounter++;
                return await nextFilter.Invoke();
            }
            
            // SIMULATE: Validation failed, short circuit and return an error response 
            var pipelineError = new PipelineError
            {
                OcurredAtStep = nameof(MakePizzaCrustFilter),
                Message = "Validation failed"
            };
            
            var response = new PipelineResponse(string.Empty, pipelineError);
            _fakeCounter++;
            return response;
        }
    }
}