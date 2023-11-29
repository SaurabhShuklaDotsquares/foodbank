using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class ForgotPasswordDtoValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidator()
        {

            RuleFor(r => r.NewPassword).NotEmpty().WithMessage("*required").Length(6, 20).WithMessage("*min 6 and max 20 charcters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must be at least 6 characters long  contain at least 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("*required")
                .Equal(r => r.NewPassword).WithMessage("*confirm password should be the same as the new password.");
            RuleFor(r => r.PasswordQuestion).NotEmpty().WithMessage("*required");
            RuleFor(r => r.PasswordAnswer).NotEmpty().WithMessage("*required");

        }
    }
}
