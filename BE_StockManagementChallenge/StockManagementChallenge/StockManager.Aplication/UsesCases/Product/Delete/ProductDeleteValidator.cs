using FluentValidation;

namespace StockManager.Application.UsesCases.Product.Delete
{
    public class ProductDeleteValidator : AbstractValidator<ProductDeleteCommand>
    {
        public ProductDeleteValidator()
        {
            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} no es valida.");
        }
    }
}
