using System.Net;
using System.Net.Mail;

namespace Backend.src.Mailer
{
    public class MailerService
    {
        public void SendEmail(string NumeroDePessoa, string Email, string Nome, string role)
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

                string body =
                    $@"
<html>
<head>
    <meta charset='UTF-8'>
</head>
<body style='font-family: Arial, sans-serif; background-color: #f7f7f7; margin: 0; padding: 20px;'>
    <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0 2px 5px rgba(0,0,0,0.1);'>
        <h1 style='color: #333333;'>Olá {Nome},</h1>
        <p style='font-size: 16px; color: #666666;'>Aqui está o seu número de pessoa:</p>
        <div style='text-align: center; margin: 30px 0;'>
            <span style='font-size: 24px; font-weight: bold; color: #4CAF50;'>{NumeroDePessoa}</span>
        </div>
        <p style='font-size: 14px; color: #999999;'>Cadastrado como: {role}</p>
        <p style='font-size: 14px; color: #333333;'>Atenciosamente,<br/>Secretaria</p>
    </div>
</body>
</html>";

                MailMessage mail = new()
                {
                    From = new MailAddress("testecsharpmailer@gmail.com"),
                    Subject = "Número de Pessoa",
                    Body = body,
                    IsBodyHtml = true,
                };

                mail.To.Add(Email);

                smtpClient.Send(mail);
            }
            catch (SmtpException smtpEx)
            {
                throw new Middlewares.Exceptions.SmtpException(
                    "Erro ao enviar email: " + smtpEx.Message
                );
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
