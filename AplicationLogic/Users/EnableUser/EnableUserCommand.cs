using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Users.EnableUser
{
    public class EnableUserCommand:IRequest
    {
        public int UserId { get; set; }
        public int CurrentUserId { get; set; }
    }
}
