using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using SM.Core.Common.Model;
using SM.Core.Interfaces.Services.Notification;
using System.Net;

namespace SM.Infrastructre.Services.Notification
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailAccountOptions _emailAccountOptions;
        public EmailSender(IOptions<EmailAccountOptions> emailAccountOptions)
        {
            _emailAccountOptions = emailAccountOptions.Value;
        }

        #region Smtp Client Build
        private async Task<SmtpClient> BuildSmtpAsync()
        {
            SmtpClient smtpClient = new SmtpClient();

            try
            {
                await smtpClient.ConnectAsync(
                   _emailAccountOptions.Host,
                    _emailAccountOptions.Port,
                    _emailAccountOptions.EnableSsl);

                if (_emailAccountOptions.Username != null)
                    await smtpClient.AuthenticateAsync(new NetworkCredential(_emailAccountOptions.Username, _emailAccountOptions.Password));
                else
                    await smtpClient.AuthenticateAsync(CredentialCache.DefaultNetworkCredentials);

                return smtpClient;
            }
            catch (Exception ex)
            {
                smtpClient.Dispose();
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion

        public async Task SendEmailAsync(string subject, string body, string fromAddress, string fromName, string toAddress, string toName)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress(fromName, fromAddress));
            message.To.Add(new MailboxAddress(toName, toAddress));
            message.Subject = subject;
            message.Body = new Multipart("mixed") { new TextPart(TextFormat.Html) { Text = body } };

            using SmtpClient smtpClient = await BuildSmtpAsync();
            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
        }
    }
}
