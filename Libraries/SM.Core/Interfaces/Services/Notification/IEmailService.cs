using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Notification
{
    public interface IEmailService
    {
        Task SendForgotPasswordEmailAsync(string refreshPasswordToken, string email);
    }
}
