using FB.Dto;
using FluentValidation;
using System;
using System.Linq;

namespace FB.Validator
{
    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(p => p.Title).Length(0, 4).WithMessage("*maximum 4 characters allowed");

            RuleFor(p => p.Surname).NotEmpty().WithMessage("*required")
                    .Length(0, 35).WithMessage("*maximum 35 characters allowed");

            RuleFor(p => p.Forenames).NotEmpty().WithMessage("*required")
                    .Length(0, 35).WithMessage("*maximum 35 characters allowed");

            RuleFor(p => p.Email).NotEmpty().WithMessage("*required");

            RuleFor(p => p.UserName).NotEmpty().WithMessage("*required");
            RuleFor(p => p.Password).NotEmpty().WithMessage("*required");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("*required");
            RuleFor(p => p.PasswordQuestion).NotEmpty().WithMessage("*required");
            RuleFor(p => p.PasswordAnswer).NotEmpty().WithMessage("*required");

            RuleFor(p => p.UserName).Length(0, 100).WithMessage("*maximum 100 characters allowed").Matches(@"[^- ]*").WithMessage(" *space or hyphen not allowed in username");

            RuleFor(p => p.Email).EmailAddress().WithMessage("*invalid email format")
                    .Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.Password).Length(6, 20).WithMessage("*min 6 and max 20 charcters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must have 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");

            RuleFor(p => p.ConfirmPassword).Length(0, 50).WithMessage("*maximum 50 characters allowed")
                    .Equal(p => p.Password).WithMessage("*confirm password should be the same as the password.");

            RuleFor(p => p.PasswordQuestion).Length(0, 300).WithMessage("*maximum 300 charachters allowed");

            RuleFor(p => p.PasswordAnswer).Length(0, 300).WithMessage("*maximum 300 characters allowed");

            RuleFor(p => p.Initials).Length(0, 15).WithMessage("*maximum 15 characters allowed");

            RuleFor(p => p.Suffix).Length(0, 15).WithMessage("*maximum 15 characters allowed");

            //RuleFor(p => p.Reference).NotEmpty().WithMessage("*required").Length(0, 15).WithMessage("*maximum 17 characters allowed");

            //RuleFor(p => p.CentralOfficeID).NotEmpty().WithMessage("*required");

            //RuleFor(p => p.CharityID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.BranchID).NotEmpty().WithMessage("*required");
        }
    }
}
