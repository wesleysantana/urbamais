using Microsoft.EntityFrameworkCore;
using Urbamais.Domain.Entities.EntitiesOfCore;
using Urbamais.Domain.Entities.Financeiro;
using Urbamais.Domain.Entities.Fornecedores;
using Urbamais.Domain.Entities.Obras;
using Urbamais.Domain.Entities.Planejamentos;
using Urbamais.Domain.Entities.Suprimentos;
using Urbamais.Infra.Config.ConfigModels;

namespace Urbamais.Infra.Config;

public class ContextEf : DbContext
{
    public ContextEf(DbContextOptions<ContextEf> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<CentroCusto> CentrosCusto { get; set; }
    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Equipamento> Equipamentos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }
    public DbSet<Insumo> Insumos { get; set; }
    public DbSet<Obra> Obras { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<Planejamento> Planejamentos { get; set; }
    public DbSet<PlanejamentoInsumo> PlanejamentosInsumos { get; set; }
    public DbSet<RegistroFinanceiro> RegistrosFinanceiros { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
    public DbSet<Unidade> Unidades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = new CentroCustoConfig(modelBuilder);
        _ = new EnderecoConfig(modelBuilder);
        _ = new CidadeConfig(modelBuilder);
        _ = new ColaboradorConfig(modelBuilder);
        _ = new EmpresaConfig(modelBuilder);
        _ = new ObraConfig(modelBuilder);
        _ = new EmailConfig(modelBuilder);
        _ = new EquipamentoConfig(modelBuilder);
        _ = new InsumoConfig(modelBuilder);
        _ = new PedidoConfig(modelBuilder);
        _ = new TelefoneConfig(modelBuilder);
        _ = new PlanejamentoConfig(modelBuilder);
        _ = new PlanejamentoInsumoConfig(modelBuilder);
        _ = new RegistroFinanceiroConfig(modelBuilder);
        _ = new CompraConfig(modelBuilder);
        _ = new FornecedorConfig(modelBuilder);
        _ = new UnidadeConfig(modelBuilder);
    }
}