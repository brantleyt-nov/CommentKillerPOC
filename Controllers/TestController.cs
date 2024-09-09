using CommentKillerPOC.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CommentKillerPOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController
    {
        [HttpGet("default")]
        [DataLimit]
        public int[] DefaultMethod(DateTime startTime, DateTime endTime)
        {
            return new int[] { 1, 2, 3 };
        }

        [HttpGet("overridden")]
        [DataLimit(typeof(OverriddenTimespanGetter))]
        public int[] OverriddenMethod(DateTime startDate, DateTime endDate)
        {
            return new int[] { 1, 2, 3 };
        }
    }

    public class OverriddenTimespanGetter : TimespanGetter
    {
        public override TimeSpan GetTimespan(HttpContext context)
        {
            var startTime = DateTime.Parse(context.Request.Query["startDate"].ToString());
            var endTime = DateTime.Parse(context.Request.Query["endDate"].ToString());

            return endTime - startTime;
        }
    }
}
