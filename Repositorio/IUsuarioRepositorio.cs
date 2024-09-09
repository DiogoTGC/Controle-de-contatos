using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio;

public interface IUsuarioRepositorio
{
    UsuarioModel? BuscarPorId(int id);
    UsuarioModel? BuscarPorLogin(string login);
    UsuarioModel? BuscarPorLoginEmail(string login, string email);
    List<UsuarioModel>? BuscarTodos();
    UsuarioModel Adicionar(UsuarioModel usuario);
    UsuarioModel Atualizar(UsuarioModel usuario);
    UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha);
    bool Apagar(int id);
}