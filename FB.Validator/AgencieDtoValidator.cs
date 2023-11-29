using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class AgencieDtoValidator : AbstractValidator<AgenciesDto>
    {
        public AgencieDtoValidator()
        {
            RuleFor(p => p.AgencyName).NotEmpty().WithMessage("*required").Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.BriefSummary).NotEmpty().WithMessage("*required");

            RuleFor(p => p.ContactNumber).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 charcters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character");

            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                        .Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");
        }
    }
}
