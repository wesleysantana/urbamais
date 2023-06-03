using Core.Domain;
using Microsoft.EntityFrameworkCore;
using Urbamais.Infra.Config.ConfigModels;

namespace Urbamais.Infra.Config;

public class ContextEf : DbContext
{
    public ContextEf(DbContextOptions<ContextEf> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Cidade> Cidades { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Telefone> Telefones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        CidadeConfig.Config(modelBuilder);
        EmailConfig.Config(modelBuilder);
        EnderecoConfig.Config(modelBuilder);
        TelefoneConfig.Config(modelBuilder);
    }
}