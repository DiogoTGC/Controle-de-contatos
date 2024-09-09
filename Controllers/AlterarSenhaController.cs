using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao) : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;
    private readonly ISessao _sessao = sessao;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
    {
        try
        {
            UsuarioModel? usuarioLogado = _sessao.BuscarSessaoUsuario();

            if (usuarioLogado != null) alterarSenhaModel.Id = usuarioLogado.Id;

            if (ModelState.IsValid)
            {
                _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                TempData["MensagemSucesso"] = "Senha alterada com sucesso.";
            }

            return View("Index", alterarSenhaModel);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Não conseguimos alterar sua senha, tente novamente. Detalhe do erro: {erro.Message}";
            return View("Index", alterarSenhaModel);
        }
    }
}