using FluentValidation;
using MinimalApi.Model;

namespace MinimalApi.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(t => t.Value)
                .NotEmpty()
                .MinimumLength(5)
                .WithMessage("Value of order must be atleast 5 characters");
        }
    }
}
