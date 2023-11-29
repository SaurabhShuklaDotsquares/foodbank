using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class FamilyParcelDtoValidator : AbstractValidator<FamilyParcelDto>
    {
        public FamilyParcelDtoValidator()
        {
            RuleFor(p => p.FamilyId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.ParcelTypeId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.StandardParcelId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.PackerId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.DeliverrerId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.DeliveryDate).NotEmpty().WithMessage("*required");
        }
    }
}
