using BussinesLogic.Entities;
using BussinesLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.RepositoryInterfaces
{
    public interface IUserRepository:IRepository<User>
    {
        Task<IEnumerable<User>> GetAllByStatus(UserStatus uStatus);
    }
}
