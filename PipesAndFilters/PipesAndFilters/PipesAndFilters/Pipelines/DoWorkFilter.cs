using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PipesAndFilters.Pipelines
{
    public class DoWorkFilter : IPipelineBehavior<PipelineContext, PipelineResponse>
    {
        private readonly ILogger<DoWorkFilter> _logger;

        public DoWorkFilter(ILogger<DoWorkFilter> logger)
        {
            _logger = logger;
        }
        
        public async Task<PipelineResponse> Handle(PipelineContext context, CancellationToken cancellationToken, RequestHandlerDelegate<PipelineResponse> nextFilter)
        {
            if ( context.Request.Values.Contains("continue"))
            {
                var result = string.Concat(context.Request.Values, " | added by DoWorkFilter | ");
                context.Request.Values = result;
                return await nextFilter.Invoke();
            }
            
            // SIMULATE: Validation failed, short circuit and return an error response 
            var pipelineError = new PipelineError
            {
                OcurredAtStep = nameof(DoWorkFilter),
                Message = "Validation failed"
            };
            
            var response = new PipelineResponse(string.Empty, pipelineError);
            return response;
        }
    }
}