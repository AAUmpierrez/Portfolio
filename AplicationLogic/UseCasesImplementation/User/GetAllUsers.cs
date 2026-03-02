using AplicationLogic.UseCasesInterface.User;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.User;
using SharedLogic.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.User
{
    public class GetAllUsers :IGetAllUsers
    {
        private IUserRepository _userRepository {  get; set; }

        public GetAllUsers(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserListDto>> Execute()
        {
            var users = await _userRepository.GetAllAsync();
            return UserMapper.UsersToUsersDto(users.ToList());
        }
    }
}
