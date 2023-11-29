using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FB.Dto;

namespace FB.Validator
{
    public class AddFamilyDtoValidation : AbstractValidator<AddFamilyDto>
    {
        public AddFamilyDtoValidation()
        {
            RuleFor(p => p.FamilyName).NotEmpty().WithMessage("*required").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.").Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                        .Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.Contactno).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 characters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed characters and special characters");
           
            //For Address Validation
            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");

            //RuleFor(p => p.ParcelDeliver).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.GDPRPreferences).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.LocalAuthCodeId).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.DeliveryNote).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.VoucherNumber).NotEmpty().WithMessage("*required");
        }
    }
    public class EditFamilyDtoValidation : AbstractValidator<EditFamilyDto>
    {
        public EditFamilyDtoValidation()
        {
            RuleFor(p => p.FamilyName).NotEmpty().WithMessage("*required").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.").Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                        .Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.Contactno).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 charcters allowed").Matches(@"[^a-zA-Z!#$%^&*()_\=\[\]{};':\\|,.<>\/?]*").WithMessage("*does not allowed character and special character");

            //For Address Validation
            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");

            //RuleFor(p => p.ParcelDeliver).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.GDPRPreferences).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.LocalAuthCodeId).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.DeliveryNote).NotEmpty().WithMessage("*required");
            //RuleFor(p => p.VoucherNumber).NotEmpty().WithMessage("*required");
        }
    }
    public class AdminEditFamilyDtoValidation : AbstractValidator<AdminEditFamilyDto>
    {
        public AdminEditFamilyDtoValidation()
        {
            RuleFor(p => p.FamilyName).NotEmpty().WithMessage("*required").Matches(@"^[a-zA-Z ]*$").WithMessage("Only characters allowed.").Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.Email).NotEmpty().WithMessage("*required").EmailAddress().WithMessage("*invalid email format")
                        .Length(0, 100).WithMessage("*maximum 100 characters allowed");
            RuleFor(p => p.Contactno).NotEmpty().WithMessage("*required").Length(10, 20).WithMessage("*min 10 and max 20 charcters allowed").Matches(@"^[0-9+]*$").WithMessage("Only numbers allowed.");

            //For Address Validation
            RuleFor(p => p.HouseName).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.HouseNumber).Length(0, 100).WithMessage("*maximum 100 characters allowed");

            RuleFor(p => p.StreetName).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed");

            RuleFor(p => p.CountryID).NotEmpty().WithMessage("*required");

            RuleFor(p => p.City).NotEmpty().WithMessage("*required").Length(0, 50).WithMessage("*maximum 50 characters allowed.");

            RuleFor(p => p.PostCode).NotEmpty().WithMessage("*required").Length(0, 10).WithMessage("*maximum 10 characters allowed");

            RuleFor(p => p.ParcelDeliverDate).NotEmpty().WithMessage("*required");
            RuleFor(p => p.GDPRPreferences).NotEmpty().WithMessage("*required");
            RuleFor(p => p.LocalAuthCodeId).NotEmpty().WithMessage("*required").Length(1, 8).WithMessage("*min 1 and max 8 charcters allowed").Matches(@"^[0-9]*$").WithMessage("Only numbers allowed.");
            RuleFor(p => p.DeliveryNote).NotEmpty().WithMessage("*required");
           
        }
    }
}
