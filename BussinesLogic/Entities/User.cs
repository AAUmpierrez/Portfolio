using BussinesLogic.Enums;
using BussinesLogic.Exceptions;
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
            Validate();
        }

        public void AddPermission(Permission permission)
        {
            if (_userPermissions.Any(up => up.PermissionId == permission.Id))
                return;

            _userPermissions.Add(new UserPermission(permission.Id));
        }

        public void RemovePermission(int permissionId)
        {
            var permission = _userPermissions
                .FirstOrDefault(up => up.PermissionId == permissionId);

            if (permission != null)
                _userPermissions.Remove(permission);
        }

        public bool HasPermission(string permissionName)
        {
            if (Role.HasPermission(permissionName))
                return true;

            return _userPermissions
                .Any(up => up.Permission.Name == permissionName);
        }

        public void AddComment(string message,bool isInternal)
        {
            if (Status == UserStatus.Inactive)
                throw new InvalidOperationException("Error. User inactive");

            var comment = new UserComment(Id,message,isInternal);
            Comments.Add(comment);
        }

        public void Disable(int disableBy)
        {
            if (Status == UserStatus.Inactive) throw new UserException("Error. User is inactive");
            if (disableBy <= 0) throw new UserException("Error. User not valid");
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
            Role = newRole;
        }

        private void Validate()
        {
            if (string.IsNullOrEmpty(FirstName)) throw new UserException("Error. First name can not be empty");
            if (string.IsNullOrEmpty(LastName)) throw new UserException("Error. Last name can not be empty");
            if (string.IsNullOrEmpty(Email)) throw new UserException("Error. Emial name can not be empty");
            if (string.IsNullOrEmpty(Password)) throw new UserException("Error. Password name can not be empty");
        }
    }
}
