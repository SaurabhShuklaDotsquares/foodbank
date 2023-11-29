using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class DeclarationDtoValidator : AbstractValidator<DeclarationDto>
    {
        public DeclarationDtoValidator()
        {
            RuleFor(p => p.DeclarationDate).NotEmpty().WithMessage("*required");
            RuleFor(p => p.ValidForm).NotEmpty().WithMessage("*required");
            RuleFor(p => p.ValidTo).NotEmpty().WithMessage("*required");
        }
    }
}
