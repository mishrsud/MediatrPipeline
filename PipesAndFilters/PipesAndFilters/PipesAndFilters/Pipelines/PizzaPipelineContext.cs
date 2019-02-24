using MediatR;

namespace PipesAndFilters.Pipelines
{
    public class PizzaPipelineContext : IRequest<PipelineResponse>
    {
        public PiplineRequest Request { get; }
        public string CurrentStep { get; set; }

        public string Garnish { get; private set; }

        public PizzaBaseType PizzaBase { get; private set; }

        public PizzaBaseFlourType PizzaBaseFlour { get; private set; }
        
        public PizzaPipelineContext(PiplineRequest request)
        {
            Request = request;
        }

        public void SetPizzaBase(PizzaBaseFlourType pizzaBaseFlourType, PizzaBaseType pizzaBaseType)
        {
            PizzaBaseFlour = pizzaBaseFlourType;
            PizzaBase = pizzaBaseType;
        }

        public void AddGarnish(string garnish)
        {
            Garnish = garnish;
        }
    }
}