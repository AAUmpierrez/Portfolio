using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using SharedLogic.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class UserUpdateCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private IUserRepository _userRepository {  get; set; }
        public UserUpdateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Execute(UpdateUserCommand command)
        {
            if (command == null) throw new BadRequestException("Error. Incorrect user data");
            await _userRepository.UpdateAsync(UserMapper.UpdateUserComandToUser(command));
        }
    }
}
