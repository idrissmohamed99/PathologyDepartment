using ExchengeFoodAPI.Models;
using Infra.DTOs;
using Infra.ValidationServices.IAuthUserValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace ExchengeFoodAPI.Filters.AuthUserFilter
{
    public class AuthUserFilterAttribute : ActionFilterAttribute
    {
        private readonly IUserAuthValidationServices _userAuthValidationServices;
        public AuthUserFilterAttribute(IUserAuthValidationServices userAuthValidationServices)
        {
            _userAuthValidationServices = userAuthValidationServices;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            bool parm = context.ActionArguments.TryGetValue("userAuthModel", out object _userAuthModel);

            if (parm && _userAuthModel is UserAuthModel userAuthModel)
            {
                Infra.Domain.Users result = await _userAuthValidationServices.CheckUserIsCurrect(userAuthModel.UserName, userAuthModel.PasswordHash);

                if (result == null)
                {
                    context.Result = new BadRequestObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"تأكد من كلمة المرور او اسم المستخدم " }));
                    return;
                }
                if (await _userAuthValidationServices.CheckUserIsDelete(result.Id))
                {
                    context.Result = new BadRequestObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"هذا الحساب تم إلغاءه " }));
                    return;
                }

                if (await _userAuthValidationServices.CheckUserIsNotActive(result.Id))
                {
                    context.Result = new BadRequestObjectResult(ResultOperationDTO<bool>.
                      CreateErrorOperation(messages: new string[] { $"هذا الحساب تم إلغاء تفعيله " }));
                    return;
                }

            }



            await base.OnActionExecutionAsync(context, next);
        }

    }
}
