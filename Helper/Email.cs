using System.Net;
using System.Net.Mail;

namespace ControleDeContatos.Helper;

public class Email(IConfiguration configuration) : IEmail
{
    private readonly IConfiguration _configuration = configuration;

    public bool Enviar(string email, string assunto, string mensagem)
    {
        try
        {
            string? host = _configuration.GetValue<string>("SMTP: Host");
            string? nome = _configuration.GetValue<string>("SMTP: Nome");
            string? username = _configuration.GetValue<string>("SMTP: UserName");
            string? senha = _configuration.GetValue<string>("SMTP: Senha");
            int porta = _configuration.GetValue<int>("SMTP: Porta");

            if (username != null)
            {
                MailMessage mail = new()
                {
                    From = new MailAddress(username, nome)
                };

                mail.To.Add(email);
                mail.Subject = assunto;
                mail.Body = mensagem;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using SmtpClient smtp = new(host, porta);
                smtp.Credentials = new NetworkCredential(username, senha);
                smtp.EnableSsl = true;
                smtp.Send(mail);

                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}