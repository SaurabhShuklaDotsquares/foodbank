using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Validator
{
    public class EditPersonDtoValidator : AbstractValidator<EditPersonDto>
    {
        public EditPersonDtoValidator()
        {
            RuleFor(p => p.EditTitle).Length(0, 4).WithMessage("*maximum 4 characters allowed");

            RuleFor(p => p.Surname).NotEmpty().WithMessage("*required")
                    .Length(0, 35).WithMessage("*maximum 35 characters allowed");

            RuleFor(p => p.ForeName).NotEmpty().WithMessage("*required")
                    .Length(0, 35).WithMessage("*maximum 35 characters allowed");

            RuleFor(p => p.UserName).Length(0, 100).WithMessage("*maximum 100 characters allowed")
                    .Must(u => u != null ? (!u.Any(e => Char.IsWhiteSpace(e)) && !u.Contains("-")) : true).WithMessage("*space or hyphen not allowed in username");

            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                    .Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.UserName).NotEmpty().WithMessage("*required");

            RuleFor(p => p.EditPassword).NotEmpty().WithMessage("*required").Length(6, 20).WithMessage("*min 6 and max 20 charcters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must have 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");

            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed")
                    .Equal(p => p.EditPassword).WithMessage("*confirm password should be the same as the password.");

            RuleFor(p => p.PasswordQuestion).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 charachters allowed");

            RuleFor(p => p.PasswordAnswer).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 characters allowed");

            RuleFor(p => p.Initials).Length(0, 15).WithMessage("*maximum 15 characters allowed");

            RuleFor(p => p.Suffix).Length(0, 15).WithMessage("*maximum 15 characters allowed");

            RuleFor(p => p.Reference).NotEmpty().WithMessage("*required").Length(0, 15).WithMessage("*maximum 17 characters allowed");

            //For Address Validation
            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryId).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");
        }
    }
}
