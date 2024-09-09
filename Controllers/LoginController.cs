using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email) : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;
    private readonly ISessao _sessao = sessao;
    private readonly IEmail _email = email;

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

    public IActionResult RedefinirSenha()
    {
        return View();
    }

    [HttpPost]
    public IActionResult EnviarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel? usuario = _usuarioRepositorio.BuscarPorLoginEmail(redefinirSenhaModel.Login, redefinirSenhaModel.Email);

                if (usuario != null)
                {
                    string novaSenha = usuario.GerarNovaSenha();
                    string mensagem = $"Sua nova senha é : {novaSenha}";
                    bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de contatos - Nova senha", mensagem);

                    if (emailEnviado)
                    {
                        _usuarioRepositorio.Atualizar(usuario);
                        TempData["MensagemSucesso"] = $"Enviamos para seu email cadastrado uma nova senha.";
                    }
                    else
                    {
                        TempData["MensagemErro"] = $"Não conseguimos enviar o email. Por favor, tente novamente.";
                    }
                    return RedirectToAction("Index", "Login");
                }
                TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";
            }
            return View("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não conseguimos redefinir sua senha, tente novamente. {erro.Message}";
            return Redirect("Index");
        }
    }

    public IActionResult Sair()
    {
        _sessao.RemoverSessaoUsuario();
        return RedirectToAction("Index", "Login");
    }
}