using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class EmailService
    {
        //Lógica de criação de email para envio
        private IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CrirEmail(string[] destinatario, string assunto, int userId, string code)
        {
            Mensagem mensagem = new Mensagem(destinatario, assunto, userId, code);
            var mensagemEmailCriada = EscrevendoEmail(mensagem);
            Enviar(mensagemEmailCriada);
        }

        //Lógica de envio de email
        private void Enviar(MimeMessage mensagemEmailCriada)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    //Estabelecendo uma conexão com  cliente e passando dados necessários para efetuar a conexão.
                    client.Connect(_configuration.GetValue<string>("EmailSettings:SmtpServer"), _configuration.GetValue<int>("EmailSettings:Port"), true);
                    client.AuthenticationMechanisms.Remove("XOUATH2");
                    client.Authenticate(_configuration.GetValue<string>("EmailSettings:From"), _configuration.GetValue<string>("EmailSettings:Password"));
                    client.Send(mensagemEmailCriada);
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //Desconectando com o cliente e desalocando recurso.
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        //Lógica de criação de corpo do email.
        private MimeMessage EscrevendoEmail(Mensagem mensagem)
        {
            var mensagemEmail = new MimeMessage();
            mensagemEmail.From.Add(new MailboxAddress(_configuration.GetValue<string>("EmailSettings:From")));
            mensagemEmail.To.AddRange(mensagem.Destinatario);
            mensagemEmail.Subject = mensagem.Assunto;
            mensagemEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemEmail;
        }
    }
}