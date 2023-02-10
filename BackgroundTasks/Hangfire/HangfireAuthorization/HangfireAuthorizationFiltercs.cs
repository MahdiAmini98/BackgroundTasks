using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Hangfire.HangfireAuthorization
{
    public class HangfireAuthorizationFiltercs : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            //دسترسی به همه کاربر هایی که  login کرده اند
           var httpContext = context.GetHttpContext();
            return httpContext.User.Identity.IsAuthenticated && httpContext.User.IsInRole("Hangfire");
        }
    }
}
