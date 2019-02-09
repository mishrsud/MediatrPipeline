using MediatR;

namespace PipesAndFilters.Pipelines
{
    public class PipelineContext : IRequest<PipelineResponse>
    {
        public PiplineRequest Request { get; }

        public PipelineContext(PiplineRequest request)
        {
            Request = request;
        }
    }
}