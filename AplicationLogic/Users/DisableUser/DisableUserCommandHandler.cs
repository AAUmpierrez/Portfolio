using AplicationLogic.Interfaces;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class DisableUserCommandHandler:IRequestHandler<DisableUserCommand>
    {
        private IUserRepository _repository {  get; set; }

        public DisableUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DisableUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var user = await _repository.GetAsync(request.UserId);
            if (user == null) throw new NotFoundException($"User {request.UserId} not found");
            user.Disable(request.DisableBy);
            await _repository.UpdateAsync(user);
        }
    }
}
