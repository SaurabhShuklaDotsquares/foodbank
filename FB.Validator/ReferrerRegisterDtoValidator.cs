using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Validator
{
    public class ReferrerRegisterDtoValidator :  AbstractValidator<ReferrerRegisterDto>
    {
        public ReferrerRegisterDtoValidator()
        {

            RuleFor(p => p.UserName)
                        .Length(0, 35).WithMessage("*maximum 35 characters allowed").NotEmpty().WithMessage("*required").Must(u => u != null ? (!u.Any(e => Char.IsWhiteSpace(e)) && !u.Contains("-")) : true).WithMessage("*space or hyphen not allowed in username"); 
            
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("*required")
                        .Length(0, 35).WithMessage("*maximum 35 characters allowed").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("*required")
                        .Length(0, 35).WithMessage("*maximum 35 characters allowed").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");
            //RuleFor(p => p.OrganisationName).NotEmpty().WithMessage("*required")
            //            .Length(0, 35).WithMessage("*maximum 35 characters allowed").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");

            RuleFor(p => p.ContactNumber).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 charcters allowed").Matches(@"^[0-9+]*$").WithMessage("Only numbers allowed.")/*.Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character")*/;


            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                        .Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.EditPassword).NotEmpty().WithMessage("*required").Length(6, 20).WithMessage("*min 6 and max 20 charcters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must have 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");

            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed")
                        .Equal(p => p.EditPassword).WithMessage("*confirm password should be the same as the password.");

            RuleFor(p => p.PasswordQuestion).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 charachters allowed");

            RuleFor(p => p.PasswordAnswer).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 characters allowed");

            //For Address Validation
            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");
        }
    }
}
