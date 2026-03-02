using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Exceptions
{
    public class PermissionException : Exception
    {
        public PermissionException()
        {
        }

        public PermissionException(string? message) : base(message)
        {
        }

        public PermissionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
