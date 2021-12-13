using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EnviarEmail(string[] destinatario, string assunto, int usuarioId, string code)
        {
            Message message = new Message(destinatario, assunto, usuarioId, code);

            var mensagemDeEmail = CriarCorpoDoEmail(message);
            EnviarEmail(mensagemDeEmail);
        }

        private void EnviarEmail(MimeMessage mensagemDeEmail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));
                    client.Send(mensagemDeEmail);
                }
                catch (System.Exception)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriarCorpoDoEmail(Message message)
        {
            var mensagemDeEmail = new MimeMessage();
            mensagemDeEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            mensagemDeEmail.To.AddRange(message.Recipients);
            mensagemDeEmail.Subject = message.Subject;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return mensagemDeEmail;
        }
    }
}
