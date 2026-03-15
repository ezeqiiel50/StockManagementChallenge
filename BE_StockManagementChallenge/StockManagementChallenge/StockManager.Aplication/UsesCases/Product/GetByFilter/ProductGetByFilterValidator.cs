using FluentValidation;

namespace StockManager.Application.UsesCases.Product.GetByFilter
{
    public class ProductGetByFilterValidator : AbstractValidator<ProductGetByFilterQuery>
    {
        public ProductGetByFilterValidator()
        {
            RuleFor(x => x.Monto)
                .GreaterThan(0)
                .Must(precio => precio <= 1000000)
                .WithMessage("{PropertyName} no es valido.");
        }
    }
}
