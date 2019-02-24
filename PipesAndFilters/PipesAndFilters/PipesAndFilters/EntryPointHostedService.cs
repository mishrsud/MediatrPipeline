using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using PipesAndFilters.Pipelines;

namespace PipesAndFilters
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Invoked by runtime")]
    public class EntryPointHostedService : HostedServiceBase
    {
        private readonly ILogger<EntryPointHostedService> _logger;
        private readonly IMediator _dispatcher;

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
                var triggerRequest = BuildTriggerRequest();
                var pipeContext = new PizzaPipelineContext(triggerRequest);
                var pipelineProcessingResult = await _dispatcher.Send(pipeContext, cancellationToken);

                _logger.LogInformation("Pipeline finished, result: {pipelineProcessingResult} | Has Error: {HasErrors}",
                    pipelineProcessingResult.Result, pipelineProcessingResult.HasErrors);
                await Task.Delay(2000, cancellationToken);
            }
        }

        private static PiplineRequest BuildTriggerRequest()
        {
            return new PiplineRequest {Values = "continue"};
        }
    }
}