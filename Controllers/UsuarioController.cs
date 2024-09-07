using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers;

public class UsuarioController(IUsuarioRepositorio usuarioRepositorio) : Controller
{
    private readonly IUsuarioRepositorio _usuarioRepositorio = usuarioRepositorio;
    public IActionResult Index()
    {
        List<UsuarioModel>? usuarios = _usuarioRepositorio.BuscarTodos();

        return View(usuarios);
    }

    public IActionResult Criar()
    {
        return View();
    }

    public IActionResult Editar(int id)
    {
        UsuarioModel? usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario);
    }

    public IActionResult ApagarConfirmacao(int id)
    {
        UsuarioModel? usuario = _usuarioRepositorio.ListarPorId(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult Criar(UsuarioModel usuario)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _usuarioRepositorio.Adicionar(usuario);
                TempData["MensagemSucesso"] = "Usuario criado com sucesso";
                return RedirectToAction("Index");
            }

            return View(usuario);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Falha ao cadastrar o usuario, tente novamente. Detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenha)
    {
        try
        {
            UsuarioModel? usuario = _usuarioRepositorio.ListarPorId(usuarioSemSenha.Id);
            if (ModelState.IsValid)
            {
                if (usuario != null)
                {
                    usuario.Id = usuarioSemSenha.Id;
                    usuario.Nome = usuarioSemSenha.Nome;
                    usuario.Email = usuarioSemSenha.Email;
                    usuario.Login = usuarioSemSenha.Login;
                    usuario.Perfil = usuarioSemSenha.Perfil;

                    _usuarioRepositorio.Atualizar(usuario);
                }

                TempData["MensagemSucesso"] = "Usuario alterado com sucesso";
                return RedirectToAction("Index");
            }

            return View("Editar", usuario);
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Falha ao alterar o usuario, tente novamente. Detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }

    public IActionResult Apagar(int id)
    {
        try
        {
            bool apagado = _usuarioRepositorio.Apagar(id);

            if (apagado)
            {
                TempData["MensagemSucesso"] = "Usuario apagado com sucesso";
            }
            else
            {
                TempData["MensagemErro"] = $"Falha ao apagar o usuario, tente novamente.";
            }
            return RedirectToAction("Index");
        }
        catch (Exception erro)
        {
            TempData["MensagemErro"] = $"Falha ao apagar o usuario, tente novamente. Detalhe do erro: {erro.Message}";
            return RedirectToAction("Index");
        }
    }
}
