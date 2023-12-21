using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
  public class AuthDbContext : IdentityDbContext
  {
    public AuthDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      var readerRoleId = "44171d02-a959-49ef-9b2e-32839d77fe42";
      var writerRoleId = "58aff9b6-7ff4-49df-b749-c99df7ad1547";

      // create reader and writer roles
      var roles = new List<IdentityRole>
      {
        new IdentityRole()
        {
          Id = readerRoleId,
          Name = "Reader",
          NormalizedName = "Reader".ToUpper(),
          ConcurrencyStamp = readerRoleId
        },
        new IdentityRole()
        {
          Id = writerRoleId,
          Name = "Writer",
          NormalizedName = "Writer".ToUpper(),
          ConcurrencyStamp = writerRoleId
        },
      };

      // seed roles
      builder.Entity<IdentityRole>().HasData(roles);

      // create admin user
      var adminUserId = "a10e1d43-4f9e-407d-9fe3-3b2a58bd900b";
      var admin = new IdentityUser()
      {
        Id = adminUserId,
        UserName = "admin@codepulse.com",
        Email = "admin@codepulse.com",
        NormalizedEmail = "admin@codepulse.com".ToUpper(),
        NormalizedUserName = "admin@codepulse.com".ToUpper()
      };

      admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

      builder.Entity<IdentityUser>().HasData(admin);

      // give roles to admin
      var adminRoles = new List<IdentityUserRole<string>>()
      {
        new()
        {
          UserId = adminUserId,
          RoleId = readerRoleId
        },
        new()
        {
          UserId = adminUserId,
          RoleId = writerRoleId
        }
      };

      builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
    }
  }
}