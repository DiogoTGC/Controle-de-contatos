using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models;

public class ContatoModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do contato")]
    public required string Nome { get; set; }
    [Required(ErrorMessage = "Digite o email do contato")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Digite o celular do contato")]
    [Phone(ErrorMessage = "Celular inválido")]
    public required string Celular { get; set; }
    public required int? UsuarioId { get; set; }
    public UsuarioModel? Usuario { get; set; }
}