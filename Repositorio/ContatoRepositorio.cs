using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio;

public class ContatoRepositorio(BancoContext bancoContext) : IContatoRepositorio
{
    private readonly BancoContext _bancoContext = bancoContext;

    public ContatoModel? ListarPorId(int id)
    {
        ContatoModel? contato = _bancoContext.Contatos?.FirstOrDefault(x => x.Id == id);
        return contato;
    }

    public List<ContatoModel>? BuscarTodos(int usuarioId)
    {
        return _bancoContext.Contatos?.Where(x => x.UsuarioId == usuarioId).ToList();
    }

    public ContatoModel Adicionar(ContatoModel contato)
    {
        _bancoContext.Contatos?.Add(contato);
        _bancoContext.SaveChanges();

        return contato;
    }

    public ContatoModel Atualizar(ContatoModel contato)
    {
        ContatoModel? contatoDB = ListarPorId(contato.Id);

        if (contatoDB != null)
        {
            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoContext.Contatos?.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        throw new Exception("Houve um erro na atualização do contato");
    }

    public bool Apagar(int id)
    {
        ContatoModel? contatoDB = ListarPorId(id);

        if (contatoDB != null)
        {
            _bancoContext.Contatos?.Remove(contatoDB);
            _bancoContext.SaveChanges();

            return true;
        }

        throw new Exception("Houve um erro na deleção do contato");
    }
}