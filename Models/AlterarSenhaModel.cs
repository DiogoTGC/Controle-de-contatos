using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models;

public class AlterarSenhaModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite a senha atual do usuário.")]
    public required string SenhaAtual { get; set; }
    [Required(ErrorMessage = "Digite a nova senha do usuário.")]
    public required string NovaSenha { get; set; }
    [Required(ErrorMessage = "Repita a nova senha do usuário.")]
    [Compare("NovaSenha", ErrorMessage = "As senhas não são iguais.")]
    public required string ConfirmarSenha { get; set; }
}