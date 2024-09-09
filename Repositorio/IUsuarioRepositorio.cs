using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio;

public interface IUsuarioRepositorio
{
    UsuarioModel? ListarPorId(int id);
    UsuarioModel? BuscarPorLogin(string login);
    UsuarioModel? BuscarPorLoginEmail(string login, string email);
    List<UsuarioModel>? BuscarTodos();
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    bool Apagar(int id);
}