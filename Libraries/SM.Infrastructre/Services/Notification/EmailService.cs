using Microsoft.Extensions.Configuration;
using SM.Core.Interfaces.Services.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Services.Notification
{
    public class EmailService : IEmailService
    {

        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public EmailService(IEmailSender emailSender, IConfiguration configuration)
        {
            _emailSender = emailSender;  
            _configuration = configuration;
        }
        public async Task SendForgotPasswordEmailAsync(string refreshPasswordToken, string email)
        {
            string callback = _configuration["Common:Client"] + "/" + "forgot-password" + "/" + refreshPasswordToken;

            var body = "Merhaba değerli üyemiz;" + "<br>" +
                    "<p> Şifre sıfırlama talebiniz alınmıştır. Aşağıda bulunan bağlantıya tıklayarak şifre sıfırlama ekranına erişebilirsiniz." + "<br>" + "<a href=" + callback + ">" + callback + "<a/>";

            await _emailSender.SendEmailAsync("Small - Şifre sıfırlama", body, "", "", email, "");
        }
    }
}
