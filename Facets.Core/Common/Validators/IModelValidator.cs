using FluentValidation.Results;

namespace Facets.Core.Common.Validators;

public interface IModelValidator
{
    Task<ValidationResult> ValidateAsync<TValidator, TRequest>(TRequest request, CancellationToken token);
}