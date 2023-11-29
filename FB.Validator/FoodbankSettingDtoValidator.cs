using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class FoodbankSettingDtoValidator : AbstractValidator<FoodbankSettingDto>
    {
        public FoodbankSettingDtoValidator()
        {
            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                       .Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 characters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed characters and special characters");

            RuleFor(p => p.ReferralLimit).NotEmpty().WithMessage("*required").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
            RuleFor(p => p.ParcelLimit).NotEmpty().WithMessage("*required").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
        }
    }
}
