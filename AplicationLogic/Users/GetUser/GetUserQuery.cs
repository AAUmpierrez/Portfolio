using AplicationLogic.DTOs.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class GetUserQuery:IRequest<GetUserDto>
    {
       public int Id {  get; set; }
    }
}
