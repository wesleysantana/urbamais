using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Urbamais.Infra.Config.ConfigModels;

internal class UfConfig
{
    public static void Config(ModelBuilder builder)
    {
        builder.Entity<Uf>().HasData(
            new Uf("AC"),
            new Uf("AL"),
            new Uf("BA"),
            new Uf("CE"),
            new Uf("DF"),
            new Uf("ES"),
            new Uf("GO"),
            new Uf("MA"),
            new Uf("MT"),
            new Uf("MS"),
            new Uf("MG"),
            new Uf("PA"),
            new Uf("PB"),
            new Uf("PR"),
            new Uf("PE"),
            new Uf("PI"),
            new Uf("RJ"),
            new Uf("RN"),
            new Uf("RS"),
            new Uf("RO"),
            new Uf("RR"),
            new Uf("SC"),
            new Uf("SP"),
            new Uf("SE"),
            new Uf("TO")
        );
    }
}