using DSCITY.Services;
using Microsoft.EntityFrameworkCore;

namespace DSCITY.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CompanyRecord> Companies => Set<CompanyRecord>();
    public DbSet<RegistrationRecord> Registrations => Set<RegistrationRecord>();
    public DbSet<ContractRecord> Contracts => Set<ContractRecord>();
    public DbSet<PortalUser> Users => Set<PortalUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyRecord>(entity =>
        {
            entity.ToTable("Companies");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.Seq).ValueGeneratedOnAdd();
            entity.HasIndex(x => x.Seq);
        });

        modelBuilder.Entity<RegistrationRecord>(entity =>
        {
            entity.ToTable("Registrations");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.Seq).ValueGeneratedOnAdd();
            entity.HasIndex(x => x.Seq);
        });

        modelBuilder.Entity<ContractRecord>(entity =>
        {
            entity.ToTable("Contracts");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.Property(x => x.Seq).ValueGeneratedOnAdd();
            entity.HasIndex(x => x.Seq);
        });

        modelBuilder.Entity<PortalUser>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedNever();
            entity.HasIndex(x => x.Phone).IsUnique();
        });
    }
}