using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Museum.Models.DbModels;

namespace Museum.DataAccess
{
    public static class ModelBuilderExtension
    {
        static Guid userId = Guid.NewGuid();
        static Guid roleId = Guid.NewGuid();
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
            SeedUserRoles(modelBuilder);
        }
        private static void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = userId.ToString(),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                ConcurrencyStamp = "avadvd",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                TwoFactorEnabled = false,
                SecurityStamp = "avebgdfvs",
                PhoneNumber = "1234567890",
                FirstName = "as",
                LastName = "sa"
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            var str = passwordHasher.HashPassword(user, "2732011Qw!");
            user.PasswordHash = str;
            builder.Entity<ApplicationUser>().HasData(user);
        }

        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Id = roleId.ToString(),
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Warehouse",
                    ConcurrencyStamp = "2",
                    NormalizedName = "WAREHOUSE"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Shop",
                    ConcurrencyStamp = "3",
                    NormalizedName = "SHOP"
                });
        }

        private static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = roleId.ToString(),
                    UserId = userId.ToString()
                });
        }
       
    }
}
