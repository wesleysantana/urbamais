using Urbamais.Domain.Entities.Core;
using Urbamais.Domain.ValueObjects;

namespace Urbamais.Domain.Entities.Fornecedor;

public class Colaborador : BaseEntity, IAggregateRoot
{
    private List<Telefone>? _listTelefones = new();
    private List<Email>? _listEmails = new();

    public NomeVO Nome { get; private set; }
    public CpfVO Cpf { get; private set; }
    public Endereco Endereco { get; set; }
    public string NumeroCarteiraTrabalho { get; private set; }
    public string NumeroCNH { get; private set; }
    public string TipoCNH { get; private set; }
    public DateTime DataValidadeCNH { get; private set; }
    public FileStream CNH { get; private set; }
    public FileStream FichaEPI { get; private set; }
    public FileStream CarteiraTrabalho { get; private set; }
    public string NumeroExameAdmissional { get; private set; }
    public DateTime ValidadeExameAdmissional { get; private set; }
    public FileStream ExameAdmissional { get; private set; }
    public FileStream FichaRegistro { get; private set; }
    public FileStream OrdemServico { get; set; }


    public IReadOnlyCollection<Telefone> Telefones
    {
        get => _listTelefones!;
        private set => _listTelefones = value.ToList();
    }

    public IReadOnlyCollection<Email> Emails
    {
        get => _listEmails!;
        private set => _listEmails = value.ToList();
    }
}