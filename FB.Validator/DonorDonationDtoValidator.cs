using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class DonorDonationDtoValidator : AbstractValidator<DonorDonationDto>
    {
        public DonorDonationDtoValidator()
        {
            RuleFor(p => p.Refrence).NotEmpty().WithMessage("*required");
            RuleFor(p => p.Quantity).NotEmpty().WithMessage("*required").Length(1, 100).WithMessage("*Range valid from min 1 and max 100").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
            RuleFor(p => p.QuantityUnit).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");
            RuleFor(p => p.DonationDate).NotEmpty().WithMessage("*required");
            RuleFor(p => p.DonationType).NotEmpty().WithMessage("*required");
        }
    }
}
