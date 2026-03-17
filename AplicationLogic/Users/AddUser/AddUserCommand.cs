using MediatR;
using SharedLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class AddUserCommand:IRequest<int>
    {
        public string FirstName { get; set; }
        public string LasName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
    }
}
