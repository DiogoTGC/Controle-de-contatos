using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao) : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;
    private readonly ISessao _sessao = sessao;

    public IActionResult Index()
    {
        UsuarioModel? usuarioNaSessao = _sessao.BuscarSessaoUsuario();
        if (usuarioNaSessao != null) return RedirectToAction("Index", "Home");
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
                        _sessao.CriarSessaoUsuario(usuario);
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

    public IActionResult Sair()
    {
        _sessao.RemoverSessaoUsuario();
        return RedirectToAction("Index", "Login");
    }
}