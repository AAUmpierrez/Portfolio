using BussinesLogic.Enums;
using BussinesLogic.ValueObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BussinesLogic.Entities
{
    public class User
    {
        //Attributes
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; } 
        public Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? DisableAt { get; set; }
        public int? DisableBy { get; set; }

        public ICollection<Ticket> CreatedTickets { get; set; } = new List<Ticket>();
        public ICollection<Ticket> AssignedTickets { get; set; } = new List<Ticket>();
        public ICollection<UserComment> Comments { get; set; } = new List<UserComment>();
        private readonly List<UserPermission> _userPermissions = new();
        public IReadOnlyCollection<UserPermission> UserPermissions => _userPermissions.AsReadOnly();


        private User() { }

        public User(string firstName, string lasName, string email,string password,int role)
        {
            FirstName = firstName;
            LastName = lasName;
            Email = email.ToLower();
            Password = password;
            RoleId = role;
            Status = UserStatus.Active;
            CreatedAt = DateTime.Now;

        }

        public void AddPermission(Permission permission)
        {
            if (permission == null) throw new Exception("Permission not valid");
            if (_userPermissions.Any(up => up.PermissionId == permission.Id))
                return;

            _userPermissions.Add(new UserPermission(permission.Id));
        }

        public void RemovePermission(int permissionId)
        {
            if (permissionId <= 0) throw new Exception("Persmision not valid");
            var permission = _userPermissions
                .FirstOrDefault(up => up.PermissionId == permissionId);

            if (permission != null)
                _userPermissions.Remove(permission);
        }

        public bool HasPermission(string permissionName)
        {
            if (string.IsNullOrEmpty(permissionName)) throw new Exception("Permission not valid");
            if (Role.HasPermission(permissionName))
                return true;

            return _userPermissions
                .Any(up => up.Permission.Name == permissionName);
        }

        public void AddComment(string message,bool isInternal)
        {
            if (Status == UserStatus.Inactive)
                throw new InvalidOperationException($"User{FirstName + " " + LastName} is inactive");

            var comment = new UserComment(Id,message,isInternal);
            Comments.Add(comment);
        }

        public void Disable(int disableBy)
        {
            if (Status == UserStatus.Inactive) throw new Exception($"User{FirstName+" "+LastName} is inactive");
            if (disableBy <= 0) throw new Exception("User not valid");
            Status = UserStatus.Inactive;
            DisableBy = disableBy;
            DisableAt = DateTime.Now;
        }
        public void Enable(int enableBy)
        {
            if (Status == UserStatus.Active)
                return;

            Status = UserStatus.Active;

        }
        public void ChangeRole(Role newRole)
        {
            if (newRole == null) throw new Exception("Role not found");
            Role = newRole;
        }

    }
}
