using AplicationLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Interfaces.Security
{
    public interface IJwtService
    {
        string GenerateToken(LoginResponseDto usuario);
    }
}
