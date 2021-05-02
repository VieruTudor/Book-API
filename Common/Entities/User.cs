using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BookAPI.Common.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        
        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
