using System;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.Authentication.Data.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
