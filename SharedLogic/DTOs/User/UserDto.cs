using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.User
{
    public class UserDto
    {
       public string firstName {get;set;}
       public string lasName {get;set;}
       public string email {get;set;}
       public string password {get;set;}
       public int role { get; set; }
    }
}
