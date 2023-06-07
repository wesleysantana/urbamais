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
            .Property(x => x.DataCriacao)
            .HasColumnName("data_criacao")
            .IsRequired();

        builder.Entity<T>()
           .Property(x => x.DataAlteracao)
           .HasColumnName("data_alteracao");

        builder.Entity<T>()
           .Property(x => x.DataExclusao)
           .HasColumnName("data_exclusao");
    }
}