using BussinesLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class Role
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        private readonly List<RolePermission> _rolPermissions = new();

        public IReadOnlyCollection<RolePermission> RolPermissions => _rolPermissions.AsReadOnly();

        private Role() { } 

        public Role(string name)
        {
           Name = name;
        }

        public void AddPermission(Permission permission)
        {
            if (permission == null)
                throw new Exception($"Permision {permission.Name} not found");

            if (_rolPermissions.Any(rp => rp.PermissionId == permission.Id))
                return;

            _rolPermissions.Add(new RolePermission(permission.Id));
        }


        public bool HasPermission(string permissionName)
        {
            if (string.IsNullOrEmpty(permissionName)) throw new Exception("Permission name not valid");
            return _rolPermissions.Any(rp =>rp.IsEnabled && rp.Permission.Name == permissionName);
        }
    }
}
