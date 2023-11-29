using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class AddFoodDonationDtoValidator : AbstractValidator<FoodDonationDto>
    {
        public AddFoodDonationDtoValidator()
        {
            RuleFor(p => p.FoodItemName).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.FoodItemId).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.FoodCategoryId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("*required");
            RuleFor(p => p.DonorId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.DonationDate).NotEmpty().WithMessage("*required");

        }
    }
}
