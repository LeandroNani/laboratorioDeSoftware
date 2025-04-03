using System.Net;
using System.Net.Mail;

namespace Backend.API.Services
{
    public interface IEmailService
    {
        Task EnviarEmailAsync(string para, string assunto, string corpo);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task EnviarEmailAsync(string para, string assunto, string corpo)
        {
            var smtp = new SmtpClient(_config["Email:SmtpHost"])
            {
                Port = int.Parse(_config["Email:SmtpPort"] ?? "587"),
                Credentials = new NetworkCredential(_config["Email:Username"], _config["Email:Password"]),
                EnableSsl = true
            };

            var mensagem = new MailMessage(_config["Email:From"], para, assunto, corpo);
            await smtp.SendMailAsync(mensagem);
        }
    }
}
