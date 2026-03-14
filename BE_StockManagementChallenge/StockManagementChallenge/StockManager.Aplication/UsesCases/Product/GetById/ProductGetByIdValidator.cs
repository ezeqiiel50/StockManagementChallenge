using FluentValidation;

namespace StockManager.Application.UsesCases.Product.GetById
{
    public class ProductGetByIdValidator : AbstractValidator<ProductGetByIdQuery>
    {
        public ProductGetByIdValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("El valor ingresado no es valido.");
        }
    }
}
