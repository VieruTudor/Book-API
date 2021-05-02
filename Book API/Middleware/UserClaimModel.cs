using System;
using System.Collections.Generic;

namespace Book_API.Middleware
{
    public class UserClaimModel
    {
        public Guid ClaimUserId { get; set; }
        public IEnumerable<string> ClaimRoles { get; set; }
    }
}
