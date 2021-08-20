using ExchengeFoodAPI.Models.Supplier;
using Infra.DTOs;
using Infra.ValidationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Filters.Supplier
{
    public class UpdateSuppilerFilter : ActionFilterAttribute
    {
        private readonly ISupplierValidationServices _validationServices;

        public UpdateSuppilerFilter(ISupplierValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool parm = context.ActionArguments.TryGetValue("model", out object _model);

            if (parm && _model is UpdateSupplierModel model)
            {
                if (await _validationServices.IsNameExist(model.Id, model.Name))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"إسم المورد موجود مسبقا" }));
                    return;
                }

                if (await _validationServices.IsIdNumExist(model.Id, model.IdNumber))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"رقم الهوية موجود مسبقا" }));
                    return;
                }

            }

            await base.OnActionExecutionAsync(context, next);
        }

    }


}
