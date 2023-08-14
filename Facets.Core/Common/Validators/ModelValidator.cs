using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Facets.Core.Common.Validators;

public sealed class ModelValidator : IModelValidator
{
    private readonly IServiceProvider _serviceProvider;

    public ModelValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ValidationResult> ValidateAsync<TValidator, TRequest>(TRequest request, CancellationToken token)
    {
        var validator = ActivatorUtilities.CreateInstance<TValidator>(_serviceProvider) as AbstractValidator<TRequest>;

        var validationResult = await validator!.ValidateAsync(request, cancellation: token);

        return validationResult;
    }
}
