using FluentValidation;
using MediatR;

namespace Сonfectionery.API.Application.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse>
         : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;

        public ValidatorBehavior(
            IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var commandName = typeof(TRequest).Name;

            _logger.LogInformation("Validating command {CommandName}", commandName);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellationToken)));

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            if (failures.Any())
            {
                _logger.LogWarning("Validation errors - {CommandType} - Command: {Command} - Errors: {ValidationErrors}",
                    commandName, request, failures);

                throw new ValidationException($"Validation Errors for type {commandName}.", failures, true);
            }

            return await next();
        }
    }
}
