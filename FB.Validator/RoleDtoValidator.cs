using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FB.Dto;

namespace FB.Validator
{
    public class RoleDtoValidator:AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(r => r.RoleName).NotEmpty().WithMessage("*required").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.").Length(1, 50).WithMessage("*maximum 50 characters allowed"); ;
            RuleFor(r => r.Description).Length(1, 250).WithMessage("*maximum 250 characters allowed");
        }
    }
}
