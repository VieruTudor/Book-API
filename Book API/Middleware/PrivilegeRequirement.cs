using Microsoft.AspNetCore.Authorization;

namespace Book_API.Middleware
{
    public class PrivilegeRequirement : IAuthorizationRequirement
    {
        public string Role { get; set; }
        public PrivilegeRequirement(string role)
        {
            this.Role = role;
        }
    }
}