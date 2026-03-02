using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.User
{
    public class DisableUserDto
    {
        public int UserId { get; set; }
        public int DisableBy {  get; set; }
    }
}
