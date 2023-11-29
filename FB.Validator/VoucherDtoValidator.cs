using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class VoucherDtoValidator : AbstractValidator<VoucherDto>
    {
        public VoucherDtoValidator()
        {
            RuleFor(p => p.FamilyId).NotEmpty().WithMessage("*required");

        }
    }
}
