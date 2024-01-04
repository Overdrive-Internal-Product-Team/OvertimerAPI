using ClockifyCloneAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Database
{
    public static class Seeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            SeedCompany(modelBuilder);
            SeedRoles(modelBuilder);
            SeedUsers(modelBuilder);
        }        

        private static void SeedCompany(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyEntity>().HasData(
                new CompanyEntity() { 
                    Id = 1, 
                    Name = "Overdrive Software e Consultoria", 
                    CNPJ = "33143114000140", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            );
        }
        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RoleEntity>().HasData(
                new RoleEntity() { 
                    Id = 1, 
                    Name = "Admin",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                },
                new RoleEntity() { 
                    Id = 2, 
                    Name = "User",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                }
            );
        }

        private static void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity()
                {
                    Id = 1,
                    Name = "Admin",
                    Email = "admin@admin.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    active = true,
                    RoleId = 1,
                    CompanyId = 1,                    
                },
                new UserEntity()
                {
                    Id = 2,
                    Name = "User",
                    Email = "user@user.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    active = true,
                    RoleId = 2,
                    CompanyId = 1,
                }
            );
        }
    }
}
