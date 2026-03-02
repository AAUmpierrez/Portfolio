using BussinesLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class RolePermission
    {
        public int RoleId { get; private set; }
        public Role Role { get; private set; }

        public int PermissionId { get; private set; }
        public Permission Permission { get; private set; }

        public bool IsEnabled { get; private set; }
        public bool IsBase { get; private set; }

        private RolePermission() { }

        public RolePermission(int permissionId)
        {
            PermissionId = permissionId;
            IsEnabled = true;
        }

        public void Enable() => IsEnabled = true;

        public void Disable()
        {
            if (IsBase == true) throw new PermissionException("Error. Can not disable a base permission");
            IsEnabled = false;
        }
    }
}
