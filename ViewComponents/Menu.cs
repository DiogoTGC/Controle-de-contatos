using System.Text.Json;
using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.ViewComponents;

public class Menu : ViewComponent
{
    public async Task<IViewComponentResult?> InvokeAsync()
    {
        string? sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

        if (string.IsNullOrEmpty(sessaoUsuario)) return null;

        UsuarioModel? usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

        return View(usuario);
    }
}