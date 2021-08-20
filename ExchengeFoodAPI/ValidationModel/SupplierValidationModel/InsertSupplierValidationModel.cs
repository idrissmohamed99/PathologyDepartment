using ExchengeFoodAPI.Models.Supplier;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.ValidationModel.SupplierValidationModel
{
    public class InsertSupplierValidationModel : AbstractValidator<InsertSupplierModel>
    {
        public InsertSupplierValidationModel()
        {
            RuleFor(role => role.CityId).NotEmpty().WithMessage("يجب إختيار المدينة");
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب ادخال اسم المورد");
        }
    }
}
