using FB.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class FoodbankRecipeDtoValidator : AbstractValidator<FoodbankRecipeDto>
    {
        public FoodbankRecipeDtoValidator()
        {
            RuleFor(p => p.RecipeTitle).NotEmpty().WithMessage("*required").Length(0, 500).WithMessage("*maximum 500 characters allowed"); ;
            //RuleFor(p => p.Quntity).NotEmpty().WithMessage("*required");
        }
    }
}
