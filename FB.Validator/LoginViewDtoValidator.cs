using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FB.Validator
{
    public class LoginViewDtoValidator : AbstractValidator<LoginViewDto>
    {
        public LoginViewDtoValidator()
        {
            RuleFor(lvm => lvm.UserName).NotEmpty().WithMessage("*required")
                .Length(1, 50).WithMessage("*maximum 50 characters allowed").Must(u => u != null ? (!u.Any(e => Char.IsWhiteSpace(e)) && !u.Contains("-")) : true).WithMessage("*space or hyphen not allowed in username");

            RuleFor(lvm => lvm.Password).NotEmpty().WithMessage("*required")
                .Length(1, 20).WithMessage("*maximum 20 characters allowed");
        }
    }
}
