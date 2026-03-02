using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public  class UserPermission
    {
        public int UserId { get; private set; }
        public User User { get; private set; }

        public int PermissionId { get; private set; }
        public Permission Permission { get; private set; }

        private UserPermission() { }

        public UserPermission(int permissionId)
        {
            PermissionId = permissionId;
        }
    }
}
