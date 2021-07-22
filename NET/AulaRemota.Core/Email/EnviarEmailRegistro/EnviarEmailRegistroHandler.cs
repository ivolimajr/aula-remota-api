﻿using AulaRemota.Core.Configuration;
using AulaRemota.Core.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MailKit.Net.Smtp;
using MailKit.Security;
using MediatR;
using MimeKit;
using MimeKit.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Email.EnviarEmailRegistro
{
    public class EdrivingDeletarHandler : IRequestHandler<EnviarEmailRegistroInput, bool>
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EdrivingDeletarHandler(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        } 

        public async Task<bool> Handle(EnviarEmailRegistroInput request, CancellationToken cancellationToken)
        {
            string assunto = "Bem Vindo ao E-Driving";
            string corpoEmail = "Você já pode acessar nossa plataforma com seu email e a senha: " + request.Senha;
            // cria a mensagem
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailConfiguration.EmailFrom));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = assunto;
            email.Body = new TextPart(TextFormat.Html) { Text = corpoEmail };

            // envia o email
            using (var smtp = new SmtpClient()) {
                smtp.Connect(_emailConfiguration.SmtpHost, _emailConfiguration.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_emailConfiguration.SmtpUser, _emailConfiguration.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                email = null;
                return true;
            };
            
        }
    }
}