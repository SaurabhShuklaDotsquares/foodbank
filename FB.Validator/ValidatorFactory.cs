using FB.Dto;
using FB.Dto.Foodbank;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FB.Validator
{
    public class ValidatorFactory : ValidatorFactoryBase
    {
        private static Dictionary<Type, IValidator> _validators = new Dictionary<Type, IValidator>();

        static ValidatorFactory()
        {
            _validators.Add(typeof(IValidator<PersonDto>), new PersonDtoValidator());
            _validators.Add(typeof(IValidator<PersonAddressDto>), new PersonAddressDtoValidator());
            _validators.Add(typeof(IValidator<EditPersonDto>), new EditPersonDtoValidator());
            _validators.Add(typeof(IValidator<DeclarationDto>), new DeclarationDtoValidator());

            _validators.Add(typeof(IValidator<DonorDonationDto>), new DonorDonationDtoValidator());
            _validators.Add(typeof(IValidator<FamilyDTo>), new familydtovalidation());
            _validators.Add(typeof(IValidator<ReferrerProfileDto>), new ReferrerProfileDtoValidator());
            _validators.Add(typeof(IValidator<VolunteerDto>), new VolunteerProfileDtoValidator());
            _validators.Add(typeof(IValidator<AvailabilityDto>), new VolunteerAvailabilityDtoValidator());
            _validators.Add(typeof(IValidator<UnavailabilityDto>), new VolunteerUnAvailabilityDtoValidator());
            _validators.Add(typeof(IValidator<AddFamilyDto>), new AddFamilyDtoValidation());
            _validators.Add(typeof(IValidator<ReferrerRegisterDto>), new ReferrerRegisterDtoValidator());
            _validators.Add(typeof(IValidator<VolunteerRegisterDto>), new VolunteerRegisterDtoValidator());
            _validators.Add(typeof(IValidator<ForgotPasswordDto>), new ForgotPasswordDtoValidator());
            _validators.Add(typeof(IValidator<LoginViewDto>), new LoginViewDtoValidator());
            _validators.Add(typeof(IValidator<FeedbackMasterDTO>), new FeedbackMasterDtoValidator());
            _validators.Add(typeof(IValidator<UpdateProfileDto>), new FoodbankUpdateProfileDtoValidator());
            _validators.Add(typeof(IValidator<VolunteerAdminDto>), new VolunteerProfileAdminDtoValidator());
            _validators.Add(typeof(IValidator<GrantorDto>), new GrantorDtoValidator());
            _validators.Add(typeof(IValidator<AgenciesDto>), new AgencieDtoValidator());
            _validators.Add(typeof(IValidator<UserDto>), new UserDtoValidator());
            _validators.Add(typeof(IValidator<StockDto>), new AddStockDtoValidation());
            _validators.Add(typeof(IValidator<FoodDonationDto>), new AddFoodDonationDtoValidator());
            _validators.Add(typeof(IValidator<FoodbankSettingDto>), new FoodbankSettingDtoValidator());
            _validators.Add(typeof(IValidator<FoodbankRecipeDto>), new FoodbankRecipeDtoValidator());
            _validators.Add(typeof(IValidator<EditFamilyDto>), new EditFamilyDtoValidation());
            _validators.Add(typeof(IValidator<ParcelTypeDto>), new ParcelTypeDtoValidator());
            _validators.Add(typeof(IValidator<FamilyParcelDto>), new FamilyParcelDtoValidator());
            _validators.Add(typeof(IValidator<AdminEditFamilyDto>), new AdminEditFamilyDtoValidation());
            
            _validators.Add(typeof(IValidator<VoucherDto>), new VoucherDtoValidator());
            _validators.Add(typeof(IValidator<StockFoodDonationDto>), new AddFoodDonationInStockDtoValidator());
            _validators.Add(typeof(IValidator<RoleDto>), new RoleDtoValidator());

        }


        /// <summary>
        /// Creates an instance of a validator with the given type.
        /// </summary>
        /// <param name="validatorType">Type of the validator.</param>
        /// <returns>The newly created validator</returns>
        public override IValidator CreateInstance(Type validatorType)
        {
            IValidator validator;
            if (_validators.TryGetValue(validatorType, out validator))
                return validator;
            return validator;
        }
    }
}
