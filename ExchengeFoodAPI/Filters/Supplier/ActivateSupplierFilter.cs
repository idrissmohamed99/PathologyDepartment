using Infra.DTOs;
using Infra.ValidationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Filters.Supplier
{
    public class ActivateSupplierFilter : ActionFilterAttribute
    {
        private readonly ISupplierValidationServices _validationServices;

        public ActivateSupplierFilter(ISupplierValidationServices validationServices)
        {
            _validationServices = validationServices;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool parm = context.ActionArguments.TryGetValue("id", out object _id);

            if (!parm || string.IsNullOrWhiteSpace(_id?.ToString()))
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                  CreateErrorOperation(messages: new string[] { $"لم يتم إرسال معرف المورد " }));
                return;
            }

            if (parm && _id is string id)
            {
                if (await _validationServices.IsArchive(id))
                {
                    context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"بيانات المورد تم أرشفتها من قبل مستخدم أخر" }));
                    return;
                }
            }

            await base.OnActionExecutionAsync(context, next);
        }

    }

}
