using BussinesLogic.Entities;
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
        public static User UserDtoToUser(UserDto uDto) 
        {
            if (uDto == null) throw new BadRequestException("Error, the entered data is incorrect");
            User u  = new User(uDto.firstName,uDto.lasName,uDto.email,uDto.password,uDto.role);
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
