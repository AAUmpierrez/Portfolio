using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
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
    public class UserUpdateCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private IUserRepository _userRepository {  get; set; }
        public UserUpdateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var user = UserMapper.UpdateUserComandToUser(request);
            user.Id = request.UserId;
            await _userRepository.UpdateAsync(user);
        }
    }
}
