using FluentValidation;
using LiquorStoreFinalProject.ViewModels;
using LiquorStoreFinalProject.ViewModels.Account;

namespace LiquorStoreFinalProject.Validations
{
    //public class SignUpValidation: AbstractValidator<RegisterVM>
    //{
    //    public SignUpValidation()
    //    {
    //        RuleFor(a => a.Email)
    //           .NotEmpty().WithMessage("Email is required.")
    //           .EmailAddress().WithMessage("Invalid email address.");

    //        RuleFor(a => a.Password)
    //            .NotEmpty().WithMessage("Password is required.")
    //            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
    //            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
    //            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
    //            .Matches(@"\d").WithMessage("Password must contain at least one digit.")
    //            .Matches(@"\W").WithMessage("Password must contain at least one special character.");

    //        RuleFor(a => a.RepeatPassword)
    //            .Equal(a => a.Password).WithMessage("Passwords do not match.");
    //    }
    //}
}
