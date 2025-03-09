using System.Net;
using System.Net.Mail;

namespace Backend.src.Mailer
{
    public class MailerService
    {
        public void SendEmail(string NumeroDePessoa, string Email)
        {
            try
            {
                SmtpClient smtpClient = new("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(
                        "testecsharpmailer@gmail.com",
                        "ctao wnnk etnx ybqf "
                    ),
                    EnableSsl = true,
                };

                MailMessage mail = new()
                {
                    From = new MailAddress("testecsharpmailer@gmail.com"),
                    Subject = "Número de Pessoa",
                    Body =
                        $"<h1>Número de Pessoa</h1><p>Seu número de pessoa é: <strong>{NumeroDePessoa}</strong></p>",
                    IsBodyHtml = true,
                };

                mail.To.Add(Email);

                smtpClient.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
            catch (SmtpException smtpEx)
            {
                Console.WriteLine($"SMTP Error: {smtpEx.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                throw;
            }
        }
    }
}
