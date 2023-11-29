using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class ParcelTypeDtoValidator : AbstractValidator<ParcelTypeDto>
    {
        public ParcelTypeDtoValidator()
        {
            RuleFor(p => p.FoodItemId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("*required").Length(1, 100).WithMessage("*Range valid from min 1 and max 100").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
        }
    }
}
