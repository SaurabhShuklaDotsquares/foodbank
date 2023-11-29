using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class FeedbackMasterDtoValidator : AbstractValidator<FeedbackMasterDTO>
    {
        public FeedbackMasterDtoValidator()
        {
            RuleFor(p => p.FieldDescription).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*max 50 charcters allowed");
            RuleFor(p => p.FieldType).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.FieldDefaultValue).NotEmpty().WithMessage("*required");
        }
    }
}
