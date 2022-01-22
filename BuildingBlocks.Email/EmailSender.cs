using System;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
// ReSharper disable ConvertToUsingDeclaration

namespace BuildingBlocks.Email
{
    internal sealed class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;
        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void Send(EmailMessage message)
        {
            var msg = PrepareMessageFrom(message);
            using (var emailClient = new SmtpClient())
            {
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                emailClient.Send(msg);
                emailClient.Disconnect(true);
            }
        }

        public async Task SendAsync(EmailMessage message)
        {
            var msg = PrepareMessageFrom(message);
            using (var emailClient = new SmtpClient())
            {
                emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await emailClient.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                try
                {
                    await emailClient.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                    await emailClient.SendAsync(msg);
                    await emailClient.DisconnectAsync(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
            }
        }

        private MimeMessage PrepareMessageFrom(EmailMessage emailMessage)
        {
            var msg = new MimeMessage();

            var fromAddresses = new[]
            {
                new EmailAddress { Name = _emailConfiguration.Name, Address = _emailConfiguration.Email }
            };
            msg.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            msg.From.AddRange(fromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            msg.Subject = emailMessage.Subject;
            msg.Body = new TextPart(TextFormat.Html) { Text = emailMessage.Content };

            return msg;
        }
    }
}
