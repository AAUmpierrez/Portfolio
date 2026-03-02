using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Exceptions
{
    public class AuditException:Exception
    {
        public AuditException() { }

        public AuditException(string? message) : base(message)
        {
        }

        public AuditException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
