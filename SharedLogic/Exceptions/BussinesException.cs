using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.Exceptions
{
    public class BussinesException : Exception
    {
        public BussinesException()
        {
        }

        public BussinesException(string? message) : base(message)
        {
        }

        public BussinesException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
