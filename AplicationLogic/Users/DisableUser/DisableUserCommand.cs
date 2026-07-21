using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class DisableUserCommand:IRequest
    {
        public int UserId { get; set; }
        public int DisableBy { get; set; }
    }
}
