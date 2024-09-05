using System.ComponentModel.DataAnnotations;
using ControleDeContatos.Enums;

namespace ControleDeContatos.Models;

public class UsuarioSemSenhaModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do usuário")]
    public required string Nome { get; set; }
    [Required(ErrorMessage = "Digite o login do usuário")]
    public required string Login { get; set; }
    [Required(ErrorMessage = "Digite o email do usuário")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public required string Email { get; set; }
    [Required(ErrorMessage = "Escolha um perfil de usuário")]
    public PerfilEnum? Perfil { get; set; }
}