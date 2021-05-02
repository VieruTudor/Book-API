using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace Book_API.Filters.Auth
{
    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim claim;
        public ClaimRequirementFilter(Claim claim)
        {
            this.claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var hasClaim = context.HttpContext.User.Claims.Any(c => c.Type == claim.Type && c.Value == claim.Value);
            if (!hasClaim)
                context.Result = new ForbidResult();
        }
    }
}
