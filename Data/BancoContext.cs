using ControleDeContatos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Data;

public class BancoContext(DbContextOptions<BancoContext> options) : DbContext(options)
{
    public DbSet<ContatoModel>? Contatos { get; set; }
}