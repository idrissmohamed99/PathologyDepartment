using Infra.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Utili
{
    public class EventHandlerUtili : JwtBearerEvents
    {

        public override async Task TokenValidated(TokenValidatedContext context)
        {
            await base.TokenValidated(context);
        }

        public override Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            var result = new
            {
                Messages = "انتهت صلاحية الدخول",
                StatusCode = -2
            };
            ResultOperationDTO<string> resultOperation = ResultOperationDTO<string>.CreateErrorOperation(messages: new string[] {
                result.Messages
            });

            string json = JsonConvert.SerializeObject(resultOperation);
            context.Response.WriteAsync(json);
            return Task.CompletedTask;
        }

        public override Task Challenge(JwtBearerChallengeContext context)
        {
            if (!TryRetrieveToken(context.Request, out string token))
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                var result = new
                {
                    Messages = "لاتملك صلاحية الدخول",
                    StatusCode = -1
                };
                ResultOperationDTO<string> resultOperation = ResultOperationDTO<string>.CreateErrorOperation(messages: new string[] {
                result.Messages
                });
                string json = JsonConvert.SerializeObject(resultOperation);
                context.Response.WriteAsync(json);
            }
            return Task.CompletedTask;
        }

        public override Task MessageReceived(MessageReceivedContext context)
        {
            return base.MessageReceived(context);
        }

        private bool TryRetrieveToken(HttpRequest request, out string token)
        {
            token = null;
            if (!request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            string bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

    }
}
