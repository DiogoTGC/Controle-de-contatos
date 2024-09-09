using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio;

public class UsuarioRepositorio(BancoContext bancoContext) : IUsuarioRepositorio
{
    private readonly BancoContext _bancoContext = bancoContext;

    public UsuarioModel? BuscarPorId(int id)
    {
        return _bancoContext.Usuarios?.FirstOrDefault(x => x.Id == id);
    }

    public UsuarioModel? BuscarPorLogin(string login)
    {
        return _bancoContext.Usuarios?.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
    }

    public UsuarioModel? BuscarPorLoginEmail(string login, string email)
    {
        return _bancoContext.Usuarios?.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email == email);
    }

    public List<UsuarioModel>? BuscarTodos()
    {
        return _bancoContext.Usuarios?.ToList();
    }

    public UsuarioModel Adicionar(UsuarioModel usuario)
    {
        usuario.DataCadastro = DateTime.Now;
        usuario.SetSenhaHash();
        _bancoContext.Usuarios?.Add(usuario);
        _bancoContext.SaveChanges();

        return usuario;
    }

    public UsuarioModel Atualizar(UsuarioModel usuario)
    {
        UsuarioModel? usuarioDB = BuscarPorId(usuario.Id);

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

    public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenha)
    {
        UsuarioModel? usuarioDB = BuscarPorId(alterarSenha.Id);

        if (usuarioDB != null)
        {
            if (usuarioDB.SenhaValida(alterarSenha.SenhaAtual))
            {
                if (!usuarioDB.SenhaValida(alterarSenha.NovaSenha))
                {
                    usuarioDB.SetNovaSenha(alterarSenha.NovaSenha);
                    usuarioDB.DataAtualizacao = DateTime.Now;

                    _bancoContext.Usuarios?.Update(usuarioDB);
                    _bancoContext.SaveChanges();

                    return usuarioDB;
                }

                throw new Exception("Nova senha não pode ser igual a atual.");
            }

            throw new Exception("Senha atual está incorreta.");
        }

        throw new Exception("Houve um erro na atualização da senha. Usuário não encontrado.");
    }

    public bool Apagar(int id)
    {
        UsuarioModel? usuarioDB = BuscarPorId(id);

        if (usuarioDB != null)
        {
            _bancoContext.Usuarios?.Remove(usuarioDB);
            _bancoContext.SaveChanges();

            return true;
        }

        throw new Exception("Houve um erro na deleção do usuario");
    }
}