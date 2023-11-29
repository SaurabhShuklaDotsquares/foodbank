using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FB.Dto;

namespace FB.Validator
{
    public class AddStockDtoValidation : AbstractValidator<StockDto>
    {
        public AddStockDtoValidation()
        {
            //RuleFor(p => p.FoodCategoryId).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.FoodId).NotEmpty().WithMessage("*required");
            RuleFor(p => p.IsItemLowInStock).NotEmpty().WithMessage("*required");
            RuleFor(p => p.PricePerItem).NotEmpty().WithMessage("*required").Length(1, 100).WithMessage("*Range valid from min 1 and max 100").Matches(@"^[0-9.]*$").WithMessage("Only numbers allowed.");//.Length(0, 20).WithMessage("* max 10 characters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character"); 
            RuleFor(p => p.AboutServicerOffered).NotEmpty().WithMessage("*required");

            RuleFor(p => p.TotalQuantity).NotEmpty().WithMessage("*required").Length(1, 100).WithMessage("*Range valid from min 1 and max 100").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
            RuleFor(p => p.Unit).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.");
        }
    }
}
