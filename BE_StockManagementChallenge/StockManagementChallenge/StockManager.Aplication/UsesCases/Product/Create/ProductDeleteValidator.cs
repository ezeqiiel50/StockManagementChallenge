using FluentValidation;
using StockManager.Application.DTOs.Constant;

namespace StockManager.Application.UsesCases.Product.Create
{
    public class ProductDeleteValidator : AbstractValidator<ProductCreateCommand>
    {
        public ProductDeleteValidator()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} no es valido");

            RuleFor(x => x.Precio)
                .GreaterThan(0)
                .Must(precio => precio <= 1000000)
                .WithMessage("{PropertyName} no es valido.");

            RuleFor(x => x.Categoria)
               .NotEmpty()
               .NotNull()
               .Must(categoria => categoria == ItemsValor.PRODDOS || categoria == ItemsValor.PRODUNO)
               .WithMessage("{PropertyName} no es valido.");
        }
    }
}
