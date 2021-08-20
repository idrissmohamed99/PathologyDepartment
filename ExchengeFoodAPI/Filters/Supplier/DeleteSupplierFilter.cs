using Infra.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Filters.Supplier
{
    public class DeleteSupplierFilter : ActionFilterAttribute
    {
        public DeleteSupplierFilter() { }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool parm = context.ActionArguments.TryGetValue("id", out object _id);

            if (!parm || string.IsNullOrWhiteSpace(_id?.ToString()))
            {
                context.Result = new OkObjectResult(ResultOperationDTO<bool>.
                  CreateErrorOperation(messages: new string[] { $"لم يتم إرسال معرف المورد " }));
                return;
            }

            await base.OnActionExecutionAsync(context, next);
        }

    }

}
