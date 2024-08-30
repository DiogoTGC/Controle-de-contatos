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

    public IActionResult ApagarConfirmacao()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(ContatoModel contato)
    {
        _contatoRepositorio.Adicionar(contato);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Alterar(ContatoModel contato)
    {
        _contatoRepositorio.Atualizar(contato);

        return RedirectToAction("Index");
    }
}
