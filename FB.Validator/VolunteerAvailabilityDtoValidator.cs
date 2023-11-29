using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class VolunteerAvailabilityDtoValidator : AbstractValidator<AvailabilityDto>
    {
        public VolunteerAvailabilityDtoValidator()
        {
            RuleFor(p => p.FromDate).NotEmpty().WithMessage("*required");
            RuleFor(p => p.ToDate).NotEmpty().WithMessage("*required");
            RuleFor(p => p.TimeForm).NotEmpty().WithMessage("*required");
            RuleFor(p => p.TimeTo).NotEmpty().WithMessage("*required");
        }
    }
}
