using E_Commerce.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace E_Commerce.API.Helper
{
    public class CashAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _time;
        public CashAttribute(int time)
        {   
            _time = time;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cashKey = GenerateKeyFromRequest(context.HttpContext.Request);
            var _cashService = context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var cashResponse = await _cashService.GetCashResponseAsync(cashKey);
            if (cashResponse is not null)
            {
                var res = new ContentResult()
                {
                    ContentType = "application/json",
                    Content = cashResponse,
                    StatusCode = 200
                };
                context.Result = res;
                return;
            }
            var executedContext= await next();
            if (executedContext.Result is OkObjectResult response)
            {
                await _cashService.SetCashResponseAsync(cashKey, response.Value, TimeSpan.FromSeconds(_time));
            }
        }
        private string GenerateKeyFromRequest(HttpRequest request)
        {
            StringBuilder key = new StringBuilder();
            key.Append($"{request.Path}");

            foreach (var item in request.Query.OrderBy(x=>x.Key))
            {
                key.Append($"{item}");
            }
            return key.ToString();
        }
    }
}
