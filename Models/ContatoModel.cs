namespace ControleDeContatos.Models;

public class ContatoModel
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public required string Celular { get; set; }
}