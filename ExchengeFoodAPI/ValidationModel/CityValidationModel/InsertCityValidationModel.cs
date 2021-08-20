using ExchengeFoodAPI.Models.City;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.ValidationModel.CityValidationModel
{
    public class InsertCityValidationModel : AbstractValidator<InsertCityModel>
    {
        public InsertCityValidationModel()
        {
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب ادخال المدينة");

        }

    }
}
