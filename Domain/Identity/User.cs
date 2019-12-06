using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public List<UserRole> UserRoles { get; set; }
    }
}