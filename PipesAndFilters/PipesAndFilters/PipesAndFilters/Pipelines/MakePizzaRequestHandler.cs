using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PipesAndFilters.Pipelines
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Called by MediatR")]
    public class MakePizzaRequestHandler : IRequestHandler<PizzaPipelineContext, PipelineResponse>
    {
        public Task<PipelineResponse> Handle(PizzaPipelineContext context, CancellationToken cancellationToken)
        {
            var pizza = $"Base: {context.PizzaBaseFlour.ToString()}, Garnish : {context.Garnish}, Status: Baked";
            
            var response = new PipelineResponse(pizza);
            
            return Task.FromResult(response);
        }
    }
}