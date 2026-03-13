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
            if (request == null) throw new BadRequestException("Error. Invalid data for disable user");
            var user = await _repository.GetAsync(request.UserId);
            if (user == null) throw new BadRequestException("Error. User to disable not valid");
            user.Disable(request.DisableBy);
            await _repository.UpdateAsync(user);
        }
    }
}
