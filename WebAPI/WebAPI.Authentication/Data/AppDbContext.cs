using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Authentication.Data.Entities;

namespace WebAPI.Authentication.Data
{
    public class AppDbContext : IdentityDbContext<User,IdentityRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options) {}
    }
}