using MediatR;
using SharedLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class AddCommentCommand:IRequest
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }
    }
}
