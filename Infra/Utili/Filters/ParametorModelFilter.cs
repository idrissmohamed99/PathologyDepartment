using Infra.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Infra.Utili.Filters
{
    public class ParametorModelFilter : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<ErrorProperty> errorProperties = new List<ErrorProperty>();

                List<ErrorProperty> result = context.ModelState.Where(w => w.Value.Errors.Count > 0).Select(s => new ErrorProperty
                {
                    Key = s.Key,
                    Error = s.Value.Errors.SingleOrDefault().ErrorMessage
                }).ToList();

                context.Result = new BadRequestObjectResult(new ResponseError(result, StateResult.empty));

                if (result.All(a => a.Key == ""))
                {
                    context.Result = new BadRequestObjectResult("يجب ارسال البيانات المطلوبة");

                }
            }
            base.OnResultExecuting(context);
        }

    }

    internal class ResponseError
    {
        public List<ErrorProperty> Messages { get; set; }
        public StateResult State { get; set; }
        public ResponseError(List<ErrorProperty> Messages, StateResult State)
        {
            this.Messages = Messages;
            this.State = State;
        }

    }

    internal class ErrorProperty
    {
        public string Key { get; set; }
        public string Error { get; set; }
    }
}
