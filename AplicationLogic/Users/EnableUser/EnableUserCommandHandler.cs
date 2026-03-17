using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Users.EnableUser
{
    public class EnableUserCommandHandler:IRequestHandler<EnableUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public EnableUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(EnableUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var user = await _userRepository.GetAsync(request.UserId);
            if (user == null) throw new NotFoundException("User not found");
            user.Enable(request.CurrentUserId);
            await _userRepository.UpdateAsync(user);
        }
    }
}
