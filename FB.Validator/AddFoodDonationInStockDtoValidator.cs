using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class AddFoodDonationInStockDtoValidator : AbstractValidator<StockFoodDonationDto>
    {
        public AddFoodDonationInStockDtoValidator()
        {
            RuleFor(p => p.FoodItemName).NotEmpty().WithMessage("*required");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("*required").Length(1, 100).WithMessage("*Range valid from min 1 and max 100").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
            RuleFor(p => p.DonationDate).NotEmpty().WithMessage("*required");

        }
    }
}
