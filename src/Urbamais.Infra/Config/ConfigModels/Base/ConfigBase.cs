using Core.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Infra.Config.ConfigModels.Base;

internal abstract class ConfigBase<T> where T : BaseEntity
{
    public ConfigBase(ModelBuilder builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        var entity = typeof(T);

        builder.Entity<T>().ToTable(entity.Name.ToLower());
        builder.Entity<T>().Property(x => x.Id).HasColumnName("id");
        builder.Entity<T>().HasKey(x => x.Id).HasName(entity.Name.ToLower() + "_id");

        builder.Entity<T>()
            .Property(x => x.IdUserCreation)
            .HasColumnName("id_user_creation")
            .IsRequired();

        builder.Entity<T>()
            .Property(x => x.CreationDate)
            .HasColumnName("creation_date")
            .IsRequired();

        builder.Entity<T>()
            .Property(x => x.IdUserModification)
            .HasColumnName("id_user_modification");

        builder.Entity<T>()
           .Property(x => x.ModificationDate)
           .HasColumnName("modification_date");

        builder.Entity<T>()
           .Property(x => x.IdUserDeletion)
           .HasColumnName("id_user_deletion");

        builder.Entity<T>()
           .Property(x => x.DeletionDate)
           .HasColumnName("deletion_date");
    }
}