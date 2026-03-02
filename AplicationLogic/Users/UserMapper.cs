using AplicationLogic.DTOs.User;
using AplicationLogic.UseCasesInterface.User;
using BussinesLogic.Entities;
using BussinesLogic.Enums;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.Mappers
{
    public class UserMapper
    {
        public static User AddUserComandToUser(AddUserCommand command) 
        {
            if (command == null) throw new BadRequestException("Error, the entered data is incorrect");
            User u  = new User(command.firstName, command.lasName,command.email,command.password,command.role);
            return u;
        }
        public static GetUserDto UserToUserDto(User user)
        {
            return new GetUserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Rol = user.Role.Name,
                Status = user.Status.ToString(),
            };
        }

        public static User UpdateUserComandToUser (UpdateUserCommand command)
        {
            User u = new User(command.FirstName, command.LasName, command.Email, command.Password, command.Role);
            u.Status = (UserStatus)command.Status;
            return u;
        }

        public static IEnumerable<UserListDto> UsersToUsersDto(List<User> users)
        {
            return users.Select(u => new UserListDto()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Rol = u.Role.ToString()
            });
        }

    }
}
