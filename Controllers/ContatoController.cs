using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class ContatoController(IContatoRepositorio contatoRepositorio) : Controller
{
    private readonly IContatoRepositorio _contatoRepositorio = contatoRepositorio;

    public IActionResult Index()
    {
        List<ContatoModel>? contatos = _contatoRepositorio.BuscarTodos();

        return View(contatos);
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
