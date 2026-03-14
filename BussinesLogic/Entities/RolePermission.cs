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
        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

        public bool IsEnabled { get; set; }
        public bool IsBase { get; set; }

        private RolePermission() { }

        public RolePermission(int permissionId)
        {
            PermissionId = permissionId;
            IsEnabled = true;
        }

        public void Enable() => IsEnabled = true;

        public void Disable()
        {
            if (IsBase == true) throw new Exception("Can not disable a base permission");
            IsEnabled = false;
        }
    }
}
