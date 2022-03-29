using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Service.Authentication.Entities;

namespace WebAPI.Service.Authentication.Database
{
    public class AuthDbContext: IdentityDbContext<User>
    {
        public AuthDbContext(DbContextOptions options) : base(options) {}
    }
}