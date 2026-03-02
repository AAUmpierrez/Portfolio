using SharedLogic.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public interface IDisableUser
    {
        Task Execute(DisableUserDto uDto);
    }
}
