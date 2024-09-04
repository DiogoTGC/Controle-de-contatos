using System.ComponentModel.DataAnnotations;
using ControleDeContatos.Enums;

namespace ControleDeContatos.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Digite o nome do contato")]
    public string Nome { get; set; }
    public string Login { get; set; }
    [Required(ErrorMessage = "Digite o email do contato")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public string Email { get; set; }
    public PerfilEnum Perfil { get; set; }
    public string Senha { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }

}