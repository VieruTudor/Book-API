using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book_API.Middleware
{
    public class PermissionAuthHandler : AuthorizationHandler<PrivilegeRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PrivilegeRequirement requirement)
        {
            var claimsPrincipal = context.User;
            if (!AreClaimsValid(claimsPrincipal))
            {
                context.Fail();
                await Task.CompletedTask;
                return;
            }

            var userClaimsModel = ExtractUserClaims(claimsPrincipal);
            if (userClaimsModel.ClaimRoles == null)
                context.Fail();
            else
                ValidateUserPrivileges(context, requirement, userClaimsModel.ClaimRoles);
        }

        private void ValidateUserPrivileges(AuthorizationHandlerContext context, PrivilegeRequirement requirement, IEnumerable<string> claimRoles)
        {
            if (requirement.Role == Policies.All)
                if (claimRoles.Contains(Policies.User) || claimRoles.Contains(Policies.Admin))
                {
                    context.Succeed(requirement);
                    return;
                }
            if (claimRoles.Contains(requirement.Role))
                context.Succeed(requirement);
            else
                context.Fail();

        }

        private UserClaimModel ExtractUserClaims(System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            var claimRoleValues = claimsPrincipal
                .FindAll(c => c.Type.Equals(AuthConstants.ClaimRole))
                .Select(c => c.Value);
            Guid.TryParse(claimsPrincipal
                .FindFirst(c => c.Type.Equals(AuthConstants.ClaimSubject)).Value, out Guid claimSubjectValue);

            return new UserClaimModel
            {
                ClaimRoles = claimRoleValues,
                ClaimUserId = claimSubjectValue
            };
        }

        private bool AreClaimsValid(System.Security.Claims.ClaimsPrincipal claimsPrincipal)
        {
            return
                claimsPrincipal != null &&
                claimsPrincipal.Identity.IsAuthenticated &&
                claimsPrincipal.HasClaim(c => c.Type.Equals(AuthConstants.ClaimRole)) &&
                claimsPrincipal.HasClaim(c => c.Type.Equals(AuthConstants.ClaimSubject));
        }
    }
}
