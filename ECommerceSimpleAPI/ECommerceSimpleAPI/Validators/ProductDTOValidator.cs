using ECommerceSimpleAPI.DTOs;
using FluentValidation;

namespace ECommerceSimpleAPI.Validators
{
    internal class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(ent => ent.Name).Length(1, 100);
            RuleFor(ent => ent.Description).Length(0, 5000);
            RuleFor(ent => ent.Price).GreaterThan(0);
            RuleFor(ent => ent.Quantity).GreaterThanOrEqualTo(0);
        }
    }
}
