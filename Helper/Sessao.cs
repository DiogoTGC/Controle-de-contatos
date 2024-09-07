using System.Text.Json;
using ControleDeContatos.Models;

namespace ControleDeContatos.Helper;

public class Sessao(IHttpContextAccessor httpContext) : ISessao
{
    private readonly IHttpContextAccessor _httpContext = httpContext;
    public void CriarSessaoUsuario(UsuarioModel usuario)
    {
        string valor = JsonSerializer.Serialize(usuario);
        _httpContext.HttpContext?.Session.SetString("sessaoUsuarioLogado", valor);
    }

    public void RemoverSessaoUsuario()
    {
        _httpContext.HttpContext?.Session.Remove("sessaoUsuarioLogado");
    }

    public UsuarioModel? BuscarSessaoUsuario()
    {
        string? sessaoUsuario = _httpContext.HttpContext?.Session.GetString("sessaoUsuarioLogado");
        if (string.IsNullOrEmpty(sessaoUsuario)) return null;

        return JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);
    }
}