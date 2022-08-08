using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UsuariosApi.Models
{
    //Modelo de mensagem de email.
    public class Mensagem
    {
        public List<MailboxAddress> Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinatario, string assunto, int userId, string codigo)
        {
            Destinatario = new List<MailboxAddress>();
            Destinatario.AddRange(destinatario.Select(dest => new MailboxAddress(dest)));
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?UsuarioId={userId}&CodigoDeAtivacao={codigo}";
        }

        //Func<string, MailboxAddress> func = new Func<string, MailboxAddress>(ConvertStringToMailBox);

        //static MailboxAddress ConvertStringToMailBox(string element)
        //{
        //    return new MailboxAddress(element); 
        //}
    }
}