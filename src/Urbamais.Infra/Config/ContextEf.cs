using Core.SeedWork;
using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Construction;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Planning;
using Urbamais.Domain.Entities.Supplier;
using Urbamais.Domain.Entities.Supply;
using Urbamais.Domain.Entities.Suprimento;
using Urbamais.Infra.Config.ConfigModels;

namespace Urbamais.Infra.Config;

public class ContextEf : DbContext
{
    public ContextEf(DbContextOptions<ContextEf> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<City> Citys { get; set; }
    public DbSet<Collaborator> Collaborators { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Companie> Companies { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Equipment> Equipments { get; private set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Input> Inputs { get; set; }
    public DbSet<Construction> Constructions { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Planning> Plannings { get; set; }
    public DbSet<PlanningInput> PlannigInputs { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = new CityConfig(modelBuilder);
        _ = new CollaboratorConfig(modelBuilder);
        _ = new PurchaseConfig(modelBuilder);
        _ = new EmailConfig(modelBuilder);
        _ = new companieConfig(modelBuilder);
        _ = new AddressConfig(modelBuilder);
        _ = new EquipmentConfig(modelBuilder);
        _ = new SupplierConfig(modelBuilder);
        _ = new ConstructionConfig(modelBuilder);
        _ = new InputConfig(modelBuilder);
        _ = new OrderConfig(modelBuilder);
        _ = new PlanningConfig(modelBuilder);
        _ = new PlanningInputsConfig(modelBuilder);
        _ = new PhoneConfig(modelBuilder);
        _ = new UnitConfig(modelBuilder);
    }

    public override int SaveChanges()
    {
        SetData();
        return base.SaveChanges();
    }

    private void SetData()
    {
        foreach (var item in ChangeTracker.Entries())
        {
            if (item.Entity.GetType() == typeof(BaseEntity))
            {
                if (item.State == EntityState.Added)
                    item.Property(((BaseEntity)item.Entity).CreationDate.ToString()).CurrentValue = DateTime.Now;

                if (item.State == EntityState.Modified)
                    item.Property(((BaseEntity)item.Entity).ModificationDate.ToString()!).CurrentValue = DateTime.Now;
            }
        }
    }
}