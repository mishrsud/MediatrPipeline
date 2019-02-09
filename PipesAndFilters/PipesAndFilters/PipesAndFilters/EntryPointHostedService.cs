using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PipesAndFilters.Pipelines;

namespace PipesAndFilters
{
    public class EntryPointHostedService : HostedServiceBase
    {
        private readonly ILogger<EntryPointHostedService> _logger;
        private readonly IMediator _dispatcher;
        private static int _counter = 0;

        public EntryPointHostedService(
            ILogger<EntryPointHostedService> logger,
            IMediator dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        protected override async Task ExecuteLongRunningProcessAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                // We're simulating a condition that would cause the pipeline to be short-circuited 
                // alternatively. i.e., both StepOne and StepTwo execute in one pass through the pipeline
                //                      then only StepOne executes
                var triggerRequest = BuildTriggerRequest();
                var pipeContext = new PipelineContext(triggerRequest);
                var pipelineProcessingResult = await _dispatcher.Send(pipeContext, cancellationToken);

                _logger.LogInformation("Pipeline finished, result: {pipelineProcessingResult} | Has Error: {HasErrors}",
                    pipelineProcessingResult.Result, pipelineProcessingResult.HasErrors);
                await Task.Delay(2000, cancellationToken);
                _counter++;
            }
        }

        private static PiplineRequest BuildTriggerRequest()
        {
            return _counter % 5 == 0
                ? new PiplineRequest {Values = "continue"}
                : new PiplineRequest {Values = "dontdoit"};
        }
    }
}