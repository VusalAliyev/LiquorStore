using FluentValidation;
using LiquorStoreFinalProject.ViewModels;

namespace LiquorStoreFinalProject.Validations
{
    public class PaymentValidation : AbstractValidator<PaymentVM>
    {
        public PaymentValidation()
        {
            RuleFor(p => p.Address).NotEmpty().WithMessage("reyiz  is required.");
            RuleFor(p => p.CVV ).NotEmpty().WithMessage("CVV is required.");
            RuleFor(p => p.PostCode).NotEmpty().WithMessage("Post Code  is required.");
            RuleFor(p => p.CardName).NotEmpty().WithMessage("Card Name  is required.");
            RuleFor(p => p.Expiration).NotEmpty().WithMessage("Expiration  is required.");
            RuleFor(p => p.CardNumber).NotEmpty().WithMessage("Card Number  is required.");
        }
    }
}
