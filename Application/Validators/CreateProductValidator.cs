using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public class CreateProductValidator
    : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage(
                "Product Name is required")
            .MaximumLength(255);
    }
}