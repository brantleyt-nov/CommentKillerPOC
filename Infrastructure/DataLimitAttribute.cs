using CommentKillerPOC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace CommentKillerPOC.Infrastructure
{
    public abstract class TimespanGetter
    {
        public abstract TimeSpan GetTimespan(HttpContext context);
    }

    public class DefaultTimespanGetter : TimespanGetter
    {
        public override TimeSpan GetTimespan(HttpContext context)
        {
            var startTime = DateTime.Parse(context.Request.Query["startTime"].ToString());
            var endTime = DateTime.Parse(context.Request.Query["endTime"].ToString());

            return endTime - startTime;
        }
    }

    public class DataLimitAttribute : ActionFilterAttribute
    {
        private readonly Type _timespanGetter;

        public DataLimitAttribute() : this(typeof(DefaultTimespanGetter)) { }

        public DataLimitAttribute(Type timespanGetter)
        {
            _timespanGetter = timespanGetter;
        } 

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dataMonitoringService = context.HttpContext.RequestServices.GetService<IDataMonitoringService>()!;
            var id = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var timespanGetter = (TimespanGetter)Activator.CreateInstance(_timespanGetter)!;

            if (!await dataMonitoringService.CheckDataUsage(id, timespanGetter.GetTimespan(context.HttpContext)))
            {
                context.Result = new ContentResult { Content = "Data limit exceeded", StatusCode = 429 };
            }
            else
            {
                await base.OnActionExecutionAsync(context, next);
            }         
        }        
    }
}
