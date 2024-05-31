using System.ComponentModel.DataAnnotations;
using AgriEnergyConnect.Application.Abstractions.Exceptions;
using AgriEnergyConnect.Application.Abstractions.Messaging;
using MediatR;
using FluentValidation;
using MediatR;
using ValidationException = AgriEnergyConnect.Application.Abstractions.Exceptions.ValidationException;


namespace AgriEnergyConnect.Application.Abstractions.Behaviours;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        if (validationErrors.Any())
        {
            throw new ValidationException(validationErrors);
        }

        return await next();
    }
}