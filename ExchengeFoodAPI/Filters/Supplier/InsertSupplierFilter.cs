using ExchengeFoodAPI.Models.Supplier;
using Infra.DTOs;
using Infra.Utili;
using Infra.ValidationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Filters.Supplier
{
    public class InsertSupplierFilter : ActionFilterAttribute
    {
        private readonly ISupplierValidationServices _validationServices;
        private readonly HelperUtili _helper;

        public InsertSupplierFilter(ISupplierValidationServices validationServices, HelperUtili helper)
        {
            _validationServices = validationServices;
            _helper = helper;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool parm = context.ActionArguments.TryGetValue("model", out object _model);

            if (parm && _model is InsertSupplierModel model)
            {
                if (await _validationServices.IsNameExist(model.Name))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"إسم المورد موجود مسبقا" }));
                    return;
                }

                if (await _validationServices.IsIdNumExist(model.IdNumber))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"رقم الهوية موجود مسبقا" }));
                    return;
                }




                //model.UserId = _helper.GetCurrentUser().UserID;
            }

            await base.OnActionExecutionAsync(context, next);
        }

    }

}
