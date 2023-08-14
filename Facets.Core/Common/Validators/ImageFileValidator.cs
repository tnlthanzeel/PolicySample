using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Facets.Core.Common.Validators;

public sealed class ImageFileValidator : AbstractValidator<IFormFile>
{
    public ImageFileValidator()
    {
        RuleFor(r => r)
            .Cascade(CascadeMode.Stop)
            .ValidateImageFile();
    }
}
