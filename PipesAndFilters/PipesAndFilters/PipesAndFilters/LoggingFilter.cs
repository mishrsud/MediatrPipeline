using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PipesAndFilters
{
    public class LoggingFilter<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingFilter<TRequest, TResponse>> _logger;

        public LoggingFilter(ILogger<LoggingFilter<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Handling request: {requestType}", request.GetType().FullName);
            var response = await next.Invoke();
            _logger.LogInformation("Done Handling request: {requestType}", request.GetType().FullName);
            
            return response;
        }
    }
}