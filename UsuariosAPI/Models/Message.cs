using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosAPI.Models
{
    public class Message
    {
        public Message(IEnumerable<string> recipients, string subject, int usuarioId, string code)
        {
            Recipients = new List<MailboxAddress>();
            Recipients.AddRange(recipients.Select(r => new MailboxAddress(r)));
            Subject = subject;
            Content = $"http://localhost:6000/Ativar?UserId={usuarioId}&ActivationCode={code}";
        }

        public List<MailboxAddress> Recipients { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
