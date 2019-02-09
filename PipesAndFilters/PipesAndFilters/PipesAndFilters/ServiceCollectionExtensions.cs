using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace PipesAndFilters
{
    public static class ServiceCollectionExtensions
    {
        public static void AddPipeFilter<TFilter, TRequest, TResponse>(this IServiceCollection serviceCollection, ServiceLifetime lifetime)
            where TFilter : IPipelineBehavior<TRequest, TResponse>
        {
            serviceCollection.Add(new ServiceDescriptor(
                typeof(IPipelineBehavior<TRequest, TResponse>), typeof(TFilter), lifetime));
        }
    }
}