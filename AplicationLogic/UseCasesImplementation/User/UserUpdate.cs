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
    public class UserUpdate : IUpdateUser
    {
        private IUserRepository _userRepository {  get; set; }
        public UserUpdate(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Execute(UserDto uDto)
        {
            await _userRepository.UpdateAsync(UserMapper.UserDtoToUser(uDto));
        }
    }
}
