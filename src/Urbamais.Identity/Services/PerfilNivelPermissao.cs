namespace Urbamais.Identity.Services;

public class PerfilNivelPermissao
{
    private static readonly Lazy<PerfilNivelPermissao> _instance = new(() => new());

    private static Dictionary<string, NivelPermissao> _nivel = new();    

    public Dictionary<string, NivelPermissao> Nivel
    {
        get => _nivel;
        set => _nivel = value;
    }

    public static PerfilNivelPermissao Instance => _instance.Value;

    private PerfilNivelPermissao()
    {
    }

    public static void Editar(string service, NivelPermissao perfilNivel)
    {
        _nivel[service] = perfilNivel;
    }
}