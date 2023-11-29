using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class VolunteerUnAvailabilityDtoValidator : AbstractValidator<UnavailabilityDto>
    {
        public VolunteerUnAvailabilityDtoValidator()
        {
            RuleFor(p => p.FromDate).NotEmpty().WithMessage("*required");
            RuleFor(p => p.ToDate).NotEmpty().WithMessage("*required");
        }
    }
}
