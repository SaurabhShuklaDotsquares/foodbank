using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Validator
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().WithMessage("*required").Must(u => u != null ? (!u.Any(e => Char.IsWhiteSpace(e)) && !u.Contains("-")) : true).WithMessage("*space or hyphen not allowed in username");
            RuleFor(r => r.Postcode).NotEmpty().WithMessage("*required");
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("*required").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("*required").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");
            RuleFor(p => p.PrimaryMobile).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 characters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character");
            RuleFor(p => p.AlternateMobile).Length(10, 20).WithMessage("*min 10 and max 20 characters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed characters and special character");


            RuleFor(r => r.HouseName).NotEmpty().WithMessage("*required");
            RuleFor(r => r.Street).NotEmpty().WithMessage("*required");
            RuleFor(r => r.City).NotEmpty().WithMessage("*required");
            

            RuleFor(r => r.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email")
                .Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(r => r.RoleID).NotEmpty().WithMessage("*required");
           // RuleFor(r => r.CustomRoleID).NotEmpty().WithMessage("*required");
            //RuleFor(r => r.OldPassword).NotEmpty().WithMessage("*required");
            RuleFor(r => r.Password).NotEmpty().WithMessage("*required").Length(6, 20).WithMessage("*min 6 and max 20 charcters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must have 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");
            RuleFor(r => r.ConfirmPassword)
                .NotEmpty().WithMessage("*required")
                .Equal(r => r.Password).WithMessage("*confirm password should be the same as the password.");
            RuleFor(r => r.PasswordQuestion).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 characters allowed");
            RuleFor(r => r.PasswordAnswer).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 characters allowed");
        }
    }
}
