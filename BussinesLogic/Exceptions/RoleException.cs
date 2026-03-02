using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Exceptions
{
    public class RoleException : Exception
    {
        public RoleException()
        {
        }

        public RoleException(string? message) : base(message)
        {
        }

        public RoleException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
