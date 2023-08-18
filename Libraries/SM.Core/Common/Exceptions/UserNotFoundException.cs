using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Common.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("Email address or password is incorrect. Please try again.")
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }

        public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
