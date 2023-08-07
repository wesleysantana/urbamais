using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class ColaboradorConfig : ConfigBase<Colaborador>
{
    public ColaboradorConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<Colaborador>()
            .OwnsOne(x => x.Nome)
            .Property(x => x.Value)
            .HasColumnName("nome")
            .IsRequired()
            .HasMaxLength(255);

        builder.Entity<Colaborador>()
            .OwnsOne(x => x.Cpf)
            .Property(x => x.Value)
            .HasColumnName("cpf")
            .IsRequired()
            .HasMaxLength(11);

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroCTPS)
            .HasColumnName("numero_ctps")
            .IsRequired()
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.CTPS)
            .HasColumnName("ctps")
            .IsRequired()
            .HasMaxLength(25);

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroCNH)
            .HasColumnName("numero_cnh")
            .HasMaxLength(9);

        builder.Entity<Colaborador>()
            .Property(x => x.CNH)
            .HasColumnName("cnh")
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.TipoCNH)
            .HasColumnName("tipo_cnh")
            .HasMaxLength(2);

        builder.Entity<Colaborador>()
            .Property(x => x.DataExpiracaoCNH)
            .HasColumnName("data_expiracao_cnh");

        builder.Entity<Colaborador>()
            .Property(x => x.EPI)
            .HasColumnName("epi")
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.ExameAdimissional)
            .HasColumnName("exame_admissional")
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.FichaRegistro) 
            .HasColumnName("ficha_registro")
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.OrdemServico)
            .HasColumnName("ordem_servico")
            .HasMaxLength(50);

        builder.Entity<Colaborador>()
            .Property(x => x.NumeroExameAdmissional)
            .HasColumnName("numero_exame_admissional")
            .HasMaxLength(20);

        builder.Entity<Colaborador>()
            .Property(x => x.DataExpiracaoExameAdmissional)
            .HasColumnName("data_expiracao_exame_admissional");

        builder.Entity<Colaborador>()
            .HasMany(x => x.Enderecos)
            .WithMany(x => x.Colaboradores)
            .UsingEntity<Dictionary<string, object>>(
                "colaboradores_enderecos",
                x => x.HasOne<Endereco>().WithMany().HasForeignKey("endereco_id"),
                x => x.HasOne<Colaborador>().WithMany().HasForeignKey("colaborador_id")
            );

        builder.Entity<Colaborador>()
            .HasMany(x => x.Emails)
            .WithMany(x => x.Colaboradores)
            .UsingEntity<Dictionary<string, object>>(
                "colaboradores_emails",
                x => x.HasOne<Email>().WithMany().HasForeignKey("email_id"),
                x => x.HasOne<Colaborador>().WithMany().HasForeignKey("colaborador_id")
            );

        builder.Entity<Colaborador>()
           .HasMany(x => x.Telefones)
           .WithMany(x => x.Colaboradores)
           .UsingEntity<Dictionary<string, object>>(
               "colaboradores_telefones",
               x => x.HasOne<Telefone>().WithMany().HasForeignKey("telefone_id"),
               x => x.HasOne<Colaborador>().WithMany().HasForeignKey("colaborador_id")
           );
    }
}