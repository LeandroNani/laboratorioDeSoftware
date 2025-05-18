
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using sme.src.Services;

namespace sme.src.Services

{


    public class MailerService : IEmailSender
    {
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public MailerService(IConfiguration config)
        {
            // pegue usuário e senha de appsettings.json ou variáveis de ambiente
            _smtpUser = config["Mailer:User"] ?? throw new ArgumentNullException("Mailer:User", "SMTP user configuration is missing.");
            _smtpPass = config["Mailer:Pass"] ?? throw new ArgumentNullException("Mailer:Pass", "SMTP password configuration is missing.");
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(_smtpUser),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            mail.To.Add(toEmail);

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (SmtpException ex)
            {
                throw new Middlewares.Exceptions.MailerException("Falha no envio de email: " + ex.Message);
            }
        }
    }
}