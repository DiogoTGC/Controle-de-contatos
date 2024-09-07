using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio;

public class UsuarioRepositorio(BancoContext bancoContext) : IUsuarioRepositorio
{
    private readonly BancoContext _bancoContext = bancoContext;

    public UsuarioModel? ListarPorId(int id)
    {
        return _bancoContext.Usuarios?.FirstOrDefault(x => x.Id == id);
    }

    public UsuarioModel? BuscarPorLogin(string login)
    {
        return _bancoContext.Usuarios?.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
    }

    public List<UsuarioModel>? BuscarTodos()
    {
        return _bancoContext.Usuarios?.ToList();
    }

    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        usuario.DataCadastro = DateTime.Now;
        _bancoContext.Usuarios?.Add(usuario);
        _bancoContext.SaveChanges();

        return usuario;
    }

    public UsuarioModel Atualizar(UsuarioModel usuario)
    {
        UsuarioModel? usuarioDB = ListarPorId(usuario.Id);

        if (usuarioDB != null)
        {
            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios?.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        throw new Exception("Houve um erro na atualização do usuario");
    }

    public bool Apagar(int id)
    {
        UsuarioModel? usuarioDB = ListarPorId(id);

        if (usuarioDB != null)
        {
            _bancoContext.Usuarios?.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }

        throw new Exception("Houve um erro na deleção do usuario");
    }
}