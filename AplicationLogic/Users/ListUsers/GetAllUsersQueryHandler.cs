using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.User;
using SharedLogic.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class GetAllUsersQueryHandler :IQueryHandler<GetAllUsersQuery,IEnumerable<UserListDto>>
    {
        private IUserRepository _userRepository {  get; set; }

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserListDto>> Execute(GetAllUsersQuery query)
        {
            var users = await _userRepository.GetAllAsync();
            return UserMapper.UsersToUsersDto(users.ToList());
        }

    }
}
