using ClockifyCloneAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ClockifyCloneAPI.Database;
public class ClockifyCloneDbContext : DbContext
{
    public ClockifyCloneDbContext(DbContextOptions<ClockifyCloneDbContext> options) : base(options) { }

    public DbSet<CompanyEntity> Companies { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<WorkEntity> Works { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {       
        modelBuilder.Entity<WorkEntity>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Works)
                .UsingEntity(
                "WorkHasTags",
                l => l.HasOne(typeof(TagEntity)).WithMany().HasForeignKey("TagId").HasPrincipalKey(nameof(TagEntity.Id)),
                r => r.HasOne(typeof(WorkEntity)).WithMany().HasForeignKey("WorkId").HasPrincipalKey(nameof(WorkEntity.Id)),
                k => k.HasKey("TagId", "WorkId"));

        Seeder.SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    
}
