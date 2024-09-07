using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class LoginController(IUsuarioRepositorio usuarioRepositorio) : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Entrar(LoginModel loginModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel? usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                if (usuario != null)
                {
                    if (usuario.SenhaValida(loginModel.Senha))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    TempData["MensagemErro"] = $"Senha incorreta. Tente novamente.";
                }
                TempData["MensagemErro"] = $"Login e/ou senha inválidos. Tente novamente.";
            }
            return View("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não conseguimos realizar seu login, tente novamente. {erro.Message}";
            return Redirect("Index");
        }
    }
}