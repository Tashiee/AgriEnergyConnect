using AgriEnergyConnect.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

// Define a generic class LoggingBehavior that implements the IPipelineBehavior interface.
public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest  // Constrain the generic type TRequest to implement IBaseRequest interface.
    where TResponse : Result  // Constrain the generic type TResponse to be of type Result.
{
    // Define a logger field to log messages.
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    // Constructor to initialize the logger.
    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    // Implement the Handle method required by the IPipelineBehavior interface.
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Get the name of the request type.
        var requestName = request.GetType().Name;

        try
        {
            // Log information indicating the start of request execution.
            _logger.LogInformation("Executing request {RequestName}", requestName);

            // Call the next handler in the pipeline and await its result.
            var result = await next();

            // If the result indicates success, log a success message.
            if (result.IsSuccess)
            {
                _logger.LogInformation("Request {RequestName} processed successfully", requestName);
            }
            else
            {
                // If the result indicates failure, log an error message.
                _logger.LogError("Request {RequestName} processed with error", requestName);
            }

            // Return the result.
            return result;
        }
        catch (Exception exception)
        {
            // If an exception occurs during request handling, log an error message and rethrow the exception.
            _logger.LogError(exception, "Request {RequestName} processing failed", requestName);
            throw;
        }
    }
}
