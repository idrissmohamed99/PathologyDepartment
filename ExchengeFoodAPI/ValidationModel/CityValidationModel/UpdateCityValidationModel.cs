using ExchengeFoodAPI.Models.City;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.ValidationModel.CityValidationModel
{
    public class UpdateCityValidationModel : AbstractValidator<UpdateCityModel>
    {
        public UpdateCityValidationModel()
        {
            RuleFor(role => role.Id).NotEmpty().WithMessage("يجب إرسال رقم التعريف ");
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب ادخال اسم المدينة");
        }
    }

}
