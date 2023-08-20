using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Notification
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string subject, string body,
            string fromAddress, string fromName, string toAddress, string toName);
    }
}
