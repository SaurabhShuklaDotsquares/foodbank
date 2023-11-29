using FB.Dto;
using FB.Dto.Foodbank;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class GrantorDtoValidator : AbstractValidator<GrantorDto>
    {
        public GrantorDtoValidator()
        {
            RuleFor(p => p.ContactNumber).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 charcters allowed").Matches(@"^[0-9+]*$").WithMessage("Only numbers allowed.")/*.Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character")*/;

            RuleFor(p => p.ForeName).NotEmpty().WithMessage("*required").Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                        .Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.TotalAmount).NotEmpty().WithMessage("*required").Length(1, 20).WithMessage("*min 1 and max 20 charcters allowed").Matches(@"^[0-9.]*$").WithMessage("Only numbers allowed.");

            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");
        }
    }
}
