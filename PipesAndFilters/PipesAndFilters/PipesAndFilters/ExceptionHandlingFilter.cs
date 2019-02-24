using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PipesAndFilters.Pipelines;

namespace PipesAndFilters
{
    
    public class ExceptionHandlingFilter : IPipelineBehavior<PizzaPipelineContext, PipelineResponse>
    {
        private readonly ILogger<ExceptionHandlingFilter> _logger;

        public ExceptionHandlingFilter(ILogger<ExceptionHandlingFilter> logger)
        {
            _logger = logger;
        }
        
        public async Task<PipelineResponse> Handle(PizzaPipelineContext request, CancellationToken cancellationToken, RequestHandlerDelegate<PipelineResponse> next)
        {
            try
            {
                var response = await next();
                return response;
            }
            catch (Exception pipelineException)
            {
                _logger.LogError(pipelineException, "Exception while processing {request}", typeof(PiplineRequest).FullName);
                return new PipelineResponse(null, new PipelineError
                {
                    Exception = pipelineException,
                    Message = $"Exception while processing {typeof(PiplineRequest).FullName}",
                    OcurredAtStep = request.CurrentStep
                });
            }
        }
    }
}