using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SlotEase.Domain;
using SlotEase.Domain.Entities.Locations;
using SlotEase.Domain.Entities.Setting;
using SlotEase.Domain.Entities.Users;
using SlotEase.Domain.Extensions;
using SlotEase.Domain.Interfaces;
using System.Threading;

namespace SlotEase.Infrastructure;

public class SlotEaseContext : DbContext
{
    private readonly IUserService _userService;

    public SlotEaseContext(DbContextOptions<SlotEaseContext> dbContextOptions, IUserService userService = null)
     : base(dbContextOptions)
    {
        _userService = userService;
    }

    #region User      
    public DbSet<User> User { get; set; }
    #endregion User
    #region UserDetails      
    public DbSet<UserDetails> UserDetails { get; set; }
    #endregion UserDetails

    public DbSet<PickupPoints> PickupPoints { get; set; }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyGlobalFilters<IDelete>(e => !e.IsDeleted);
        modelBuilder.Entity<AppConfigurationUI>().HasIndex(u => u.Name).IsUnique();
          modelBuilder.Entity<PickupPoints>(entity =>
        {
            entity.Property(e => e.DistanceFromOffice)
                .HasPrecision(10, 2); // Specify precision and scale

            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8); // Specify precision and scale

            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8); // Specify precision and scale
        });


    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void OnBeforeSaving()
    {
        foreach (EntityEntry item in ChangeTracker.Entries())
        {
            AddAudit(item);
        }
    }

    private void AddAudit(EntityEntry entry)
    {
        switch (entry.State)
        {
            case EntityState.Deleted:
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = true;
                entry.CurrentValues["DeletedUserId"] = _userService?.GetUserId();
                entry.CurrentValues["DeletionTime"] = DateTime.UtcNow;
                break;
            case EntityState.Added:
                entry.State = EntityState.Added;
                entry.CurrentValues["IsDeleted"] = false;
                entry.CurrentValues["CreatorUserId"] = _userService?.GetUserId();
                entry.CurrentValues["CreationTime"] = DateTime.UtcNow;
                break;
            case EntityState.Modified:
                entry.State = EntityState.Modified;
                entry.CurrentValues["IsDeleted"] = false;
                entry.CurrentValues["ModifiedUserId"] = _userService?.GetUserId();
                entry.CurrentValues["ModificationTime"] = DateTime.UtcNow;
                break;

        }
    }

    public DbSet<AppConfigurationUI> AppConfigurations { get; set; }
}

public class SlotEaseContextDesignFactory : IDesignTimeDbContextFactory<SlotEaseContext>
{
    public IConfiguration Configuration { get; }

    public SlotEaseContext CreateDbContext(string[] args)
    {
        string connString = Configuration.GetConnectionString(nameof(ConfigSettings.SqlServerConnectionString));
        DbContextOptionsBuilder<SlotEaseContext> optionsBuilder = new DbContextOptionsBuilder<SlotEaseContext>()
            .UseSqlServer(connString);
        return new SlotEaseContext(optionsBuilder.Options);
    }
}
