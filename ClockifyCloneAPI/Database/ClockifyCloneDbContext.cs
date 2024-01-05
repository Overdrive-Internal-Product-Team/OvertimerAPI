using ClockifyCloneAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClockifyCloneAPI.Database;
public class ClockifyCloneDbContext : DbContext
{
    public ClockifyCloneDbContext(DbContextOptions<ClockifyCloneDbContext> options) : base(options) { }

    public DbSet<Company> Companies { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Work> Works { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {       
        modelBuilder.Entity<Work>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Works)
                .UsingEntity(
                "WorkHasTags",
                l => l.HasOne(typeof(Tag)).WithMany().HasForeignKey("TagId").HasPrincipalKey(nameof(Tag.Id)),
                r => r.HasOne(typeof(Work)).WithMany().HasForeignKey("WorkId").HasPrincipalKey(nameof(Work.Id)),
                k => k.HasKey("TagId", "WorkId"));

        Seeder.SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        UpdateTimestamps();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateTimestamps();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
        var entities = ChangeTracker.Entries()
            .Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

        foreach (var entityEntry in entities)
        {
            if (entityEntry.State == EntityState.Added)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
            }

            ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
        }
    }


}
