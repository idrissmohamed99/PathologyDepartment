using ExchengeFoodAPI.Models.Supplier;
using FluentValidation;


namespace ExchengeFoodAPI.ValidationModel.SupplierValidationModel
{
    public class UpdateSupplierValidationModel : AbstractValidator<UpdateSupplierModel>
    {
        public UpdateSupplierValidationModel()
        {
            RuleFor(role => role.Id).NotEmpty().WithMessage("يجب إرسال رقم التعريف المورد");
            RuleFor(role => role.CityId).NotEmpty().WithMessage("يجب إختيار المدينة");
            RuleFor(role => role.Name).NotEmpty().WithMessage("يجب ادخال اسم المورد");
        }
    }
}
