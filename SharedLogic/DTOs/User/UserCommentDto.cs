using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.User
{
    public class UserCommentDto
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }
    }
}
