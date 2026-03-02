using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Exceptions
{
    public class TicketException : Exception
    {
        public TicketException()
        {
        }

        public TicketException(string? message) : base(message)
        {
        }

        public TicketException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
