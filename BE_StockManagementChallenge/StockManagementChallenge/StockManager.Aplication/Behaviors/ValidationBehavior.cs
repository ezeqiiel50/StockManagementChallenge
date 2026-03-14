using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using ROP;

namespace StockManager.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior( IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .Select(v => v.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .ToList();

        if (!failures.Any())
            return await next();

        var errorMessage = string.Join(", ", failures.Select(f => f.ErrorMessage));

        _logger.LogWarning("Validation failed for {RequestType}: {Errors}",typeof(TRequest).Name,errorMessage);

        var responseType = typeof(TResponse);

        if (responseType.IsGenericType &&
            responseType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            var valueType = responseType.GetGenericArguments()[0];

            var failureMethod = typeof(Result)
                .GetMethods()
                .First(m =>
                    m.Name == nameof(Result.Failure) &&
                    m.IsGenericMethodDefinition &&
                    m.GetParameters().Length == 1 &&
                    m.GetParameters()[0].ParameterType == typeof(string))
                .MakeGenericMethod(valueType);

            return (TResponse)failureMethod.Invoke(null, new object[] { errorMessage })!;
        }

        throw new ValidationException(failures);
    }
}