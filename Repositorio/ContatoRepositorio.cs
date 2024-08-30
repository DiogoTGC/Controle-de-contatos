using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio;

public class ContatoRepositorio(BancoContext bancoContext) : IContatoRepositorio
{
    private readonly BancoContext _bancoContext = bancoContext;

    public List<ContatoModel>? BuscarTodos()
    {
        return _bancoContext.Contatos?.ToList();
    }

    public ContatoModel Adicionar(ContatoModel contato)
    {
        _bancoContext.Contatos?.Add(contato);
        _bancoContext.SaveChanges();

        return contato;
    }
}