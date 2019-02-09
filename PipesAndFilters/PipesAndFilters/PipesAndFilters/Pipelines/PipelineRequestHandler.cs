using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PipesAndFilters.Pipelines
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Called by MediatR")]
    public class PipelineRequestHandler : IRequestHandler<PipelineContext, PipelineResponse>
    {
        public Task<PipelineResponse> Handle(PipelineContext pipelineContext, CancellationToken cancellationToken)
        {
            var request = pipelineContext.Request;
            var result = string.Concat(request.Values, " | Added by Handler | ");
            var response = new PipelineResponse(result);
            
            return Task.FromResult(response);
        }
    }
}