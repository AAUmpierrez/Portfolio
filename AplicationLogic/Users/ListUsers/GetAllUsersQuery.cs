using MediatR;
using SharedLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class GetAllUsersQuery:IRequest<IEnumerable<UserListDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Status {  get; set; }
    }
}
