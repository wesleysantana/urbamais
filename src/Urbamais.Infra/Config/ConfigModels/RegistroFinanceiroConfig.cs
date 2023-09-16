using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Infra.Config.ConfigModels.Base;

namespace Urbamais.Infra.Config.ConfigModels;

internal class RegistroFinanceiroConfig : ConfigBase<RegistroFinanceiro>
{
    public RegistroFinanceiroConfig(ModelBuilder builder) : base(builder)
    {
        Config(builder);
    }

    private static void Config(ModelBuilder builder)
    {
        builder.Entity<RegistroFinanceiro>().ToTable("registro_financeiro");

        builder.Entity<RegistroFinanceiro>()
            .Property(x => x.ObraId)
            .HasColumnName("obra_id")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.FornecedorId)
            .HasColumnName("fornecedor_id")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.Fornecedor)
            .HasColumnName("fornecedor")
            .HasMaxLength(255)
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.DataEmissao)
            .HasColumnName("data_emissao")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.DataVencimento)
            .HasColumnName("data_vencimento")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.DataEntrada)
            .HasColumnName("data_entrada")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.TipoDoc)
            .HasColumnName("tipo_doc")
            .HasMaxLength(25)
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.NumeroDoc)
            .HasColumnName("numero_doc")
            .HasMaxLength(25)
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.Parcela)
            .HasColumnName("parcela")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.DataVencimento)
            .HasColumnName("data_vencimento")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.AprovacaoPagamento)
            .HasColumnName("aprovacao_pagamento")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.Valor)
            .Property(X => X.Value)
            .HasColumnName("valor")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.Total)
            .Property(X => X.Value)
            .HasColumnName("total")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.Caucao)
            .Property(X => X.Value)
            .HasColumnName("caucao")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.Desconto)
            .Property(X => X.Value)
            .HasColumnName("desconto")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.Acrescimo)
            .Property(X => X.Value)
            .HasColumnName("valor_acrescimo")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.ValorLiquido)
            .Property(X => X.Value)
            .HasColumnName("valor_liquido")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .OwnsOne(x => x.ValorBaixa)
            .Property(X => X.Value)
            .HasColumnName("valor_baixa")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .Property(X => X.Complemento)
            .HasColumnName("complemento")
            .HasMaxLength(1024);

        builder.Entity<RegistroFinanceiro>()
            .Property(x => x.CentroCustoId)
            .HasColumnName("centro_custo_id")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .HasOne(x => x.CentroCusto)
            .WithMany(x => x.RegistrosFinanceirosCentroCusto)
            .HasForeignKey(x => x.CentroCustoId);

        builder.Entity<RegistroFinanceiro>()
            .Property(x => x.ClasseFinanceiraId)
            .HasColumnName("classe_financeira_id")
            .IsRequired();

        builder.Entity<RegistroFinanceiro>()
            .HasOne(x => x.ClasseFinanceira)
            .WithMany(x => x.RegistrosFinanceirosClasseFinanceira)
            .HasForeignKey(x => x.ClasseFinanceiraId);
    }
}