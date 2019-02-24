using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PipesAndFilters.Pipelines
{
    public class ValidationFilter : IPipelineBehavior<PizzaPipelineContext, PipelineResponse>
    {
        private readonly ILogger<ValidationFilter> _logger;

        public ValidationFilter(ILogger<ValidationFilter> logger)
        {
            _logger = logger;
        }

        public async Task<PipelineResponse> Handle(PizzaPipelineContext context, CancellationToken cancellationToken, RequestHandlerDelegate<PipelineResponse> nextFilter)
        {
            context.CurrentStep = nameof(ValidationFilter);
            if (context.Request.Values.Contains("continue"))
            {
                // SIMULATE: Validation succeeded, call next step
                _logger.LogInformation("Validation succeeded");
                var result = string.Concat(context.Request.Values, " | Validation Signature | ");
                context.Request.Values = result;
                var pipelineResponse = await nextFilter.Invoke();
                return pipelineResponse;
            }
         
            
            // SIMULATE: Validation failed, short circuit and return an error response 
            var pipelineError = new PipelineError
            {
                OcurredAtStep = nameof(ValidationFilter),
                Message = "Validation failed"
            };
            
            var response = new PipelineResponse(string.Empty, pipelineError);
            return response;
        }
    }
}