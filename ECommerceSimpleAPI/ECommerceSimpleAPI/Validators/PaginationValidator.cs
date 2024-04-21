using ECommerceSimpleAPI.Models;
using FluentValidation;

namespace ECommerceSimpleAPI.Validators
{
    internal class PaginationValidator : AbstractValidator<PaginationSettings>
    {
        public PaginationValidator()
        {
            RuleFor(ent => ent.PageSize).GreaterThanOrEqualTo(0).LessThanOrEqualTo(1000);
            RuleFor(ent => ent.PageNumber).GreaterThanOrEqualTo(0);
        }
    }
}