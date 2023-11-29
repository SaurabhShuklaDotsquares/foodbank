using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class VolunteerProfileDtoValidator : AbstractValidator<VolunteerDto>
    {
        public VolunteerProfileDtoValidator()
        {
            RuleFor(p => p.WorkType).NotEmpty().WithMessage("*required");
            RuleFor(p => p.MaritalStatus).NotEmpty().WithMessage("*required");
            RuleFor(p => p.PasswordQuestion).NotEmpty().WithMessage("*required");
            RuleFor(p => p.PasswordAnswer).NotEmpty().WithMessage("*required");
            RuleFor(p => p.EditPassword).NotEmpty().WithMessage("*required").Length(6, 20).WithMessage("*min 6 and max 20 characters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must have 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");
            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed")
                                    .Equal(p => p.EditPassword).WithMessage("*confirm password should be the same as the password.");
           
           
        }
    }
    public class VolunteerProfileAdminDtoValidator : AbstractValidator<VolunteerAdminDto>
    {
        public VolunteerProfileAdminDtoValidator()
        {
            RuleFor(p => p.VolunteerName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("* max 50 characters allowed");
            RuleFor(p => p.SkillsId).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.DocPath).NotEmpty().WithMessage("*required");
            RuleFor(p => p.Mobile).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 characters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character");
            RuleFor(p => p.HowCanYouHelp).NotEmpty().WithMessage("*required").Length(0, 100).WithMessage("*max 100 characters allowed");
            RuleFor(p => p.EditPassword).NotEmpty().WithMessage("*required").Length(6, 20).WithMessage("*min 6 and max 20 charcters allowed").Matches(@"^(?=(.*\d){1})(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z\d]).{6,}$").WithMessage("The password must have 1 number, 1 upper case, 1 lower case and a special character (@#$%&+=!_)");

            RuleFor(p => p.ConfirmPassword).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed")
                    .Equal(p => p.EditPassword).WithMessage("*confirm password should be the same as the password.");

            RuleFor(p => p.PasswordQuestion).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 charachters allowed");

            RuleFor(p => p.PasswordAnswer).NotEmpty().WithMessage("*required").Length(0, 300).WithMessage("*maximum 300 characters allowed");
        }
    }

}
