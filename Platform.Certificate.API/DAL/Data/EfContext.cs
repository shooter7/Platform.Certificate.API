using Platform.Certificate.API.Models.Dbs;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Platform.Certificate.API.Common.Bases;
using Platform.Certificate.API.Common.Extensions;
using Platform.Certificate.API.Models.Objects;

namespace Platform.Certificate.API.DAL.Data;

public class EfContext : DbContext
{
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Models.Dbs.Certificate> Certificates { get; set; }
    public virtual DbSet<ChamberOfCommerce> ChamberOfCommerces { get; set; }

    public EfContext(DbContextOptions<EfContext> options) : base(options)
    {
    }

    public EfContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IEntity).IsAssignableFrom(type.ClrType))
            {
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
            }
        }

        // modelBuilder.Entity<Models.Dbs.Certificate>().OwnsOne(
        //     certificate => certificate.Exporter, ownedNavigationBuilder => { ownedNavigationBuilder.ToJson(); });
        //
        // modelBuilder.Entity<Models.Dbs.Certificate>().OwnsOne(
        //     certificate => certificate.Importer, ownedNavigationBuilder => { ownedNavigationBuilder.ToJson(); });

        modelBuilder.Entity<Models.Dbs.Certificate>().Property<Agent>(x => x.Exporter).HasConversion(
            v => JsonConvert.SerializeObject(v,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            v => JsonConvert.DeserializeObject<Agent>(v,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));

        modelBuilder.Entity<Models.Dbs.Certificate>().Property<Agent>(x => x.Importer).HasConversion(
            v => JsonConvert.SerializeObject(v,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
            v => JsonConvert.DeserializeObject<Agent>(v,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));


        modelBuilder.Entity<User>().HasData(new List<User>()
        {
            new()
            {
                Id = 1, Username = "admin", Password = "123456".HashPassword(), Fullname = "Ahmed Jassim",
                Role = "Admin"
            }
        });
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        OnBeforeSaving();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is IEntity &&
                        e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entityEntry in entries)
        {
            ((IEntity)entityEntry.Entity).UpdatedAt = DateTime.Now.ToUniversalTime();
            ((IEntity)entityEntry.Entity).IsDeleted = false;

            switch (entityEntry.State)
            {
                case EntityState.Added:
                    ((IEntity)entityEntry.Entity).CreatedAt = DateTime.Now.ToUniversalTime();
                    break;
                case EntityState.Modified:
                    ((IEntity)entityEntry.Entity).UpdatedAt = DateTime.Now.ToUniversalTime();
                    break;
                case EntityState.Deleted:
                    entityEntry.State = EntityState.Modified;
                    ((IEntity)entityEntry.Entity).IsDeleted = true;
                    break;
            }
        }
    }
}