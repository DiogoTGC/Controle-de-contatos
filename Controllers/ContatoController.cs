using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

[PaginaParaUsuarioLogado]
public class ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao) : Controller
{
    private readonly IContatoRepositorio _contatoRepositorio = contatoRepositorio;
    private readonly ISessao _sessao = sessao;

    public IActionResult Index()
    {
        UsuarioModel? usuarioLogado = _sessao.BuscarSessaoUsuario();

        if (usuarioLogado != null)
        {
            List<ContatoModel>? contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);

            return View(contatos);
        }

        return RedirectToAction("Index", "Login");
    }

    public IActionResult Criar()
    {
        return View();
    }

    public IActionResult Editar(int id)
    {
        ContatoModel? contato = _contatoRepositorio.ListarPorId(id);
        return View(contato);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        ContatoModel? contato = _contatoRepositorio.ListarPorId(id);
        return View(contato);
    }

    [HttpPost]
    public IActionResult Criar(ContatoModel contato)
    {
        try
        {
            if (ModelState.IsValid)
            {
                UsuarioModel? usuarioLogado = _sessao.BuscarSessaoUsuario();
                if (usuarioLogado != null) contato.UsuarioId = usuarioLogado.Id;

                _contatoRepositorio.Adicionar(contato);
                TempData["MensagemSucesso"] = "Contato criado com sucesso";
                return RedirectToAction("Index");
            }

            return View(contato);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Falha ao cadastrar seu contato, tente novamente. Detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Alterar(ContatoModel contato)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _contatoRepositorio.Atualizar(contato);
                TempData["MensagemSucesso"] = "Contato alterado com sucesso";
                return RedirectToAction("Index");
            }

            return View("Editar", contato);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Falha ao alterar seu contato, tente novamente. Detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _contatoRepositorio.Apagar(id);

            if (apagado)
            {
                TempData["MensagemSucesso"] = "Contato apagado com sucesso";
            }
            else
            {
                TempData["MensagemErro"] = $"Falha ao apagar seu contato, tente novamente.";
            }
            return RedirectToAction("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Falha ao apagar seu contato, tente novamente. Detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}
