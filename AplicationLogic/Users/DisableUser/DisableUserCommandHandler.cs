using AplicationLogic.Interfaces;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class DisableUserCommandHandler:ICommandHandler<DisableUserCommand>
    {
        private IUserRepository _repository {  get; set; }

        public DisableUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(DisableUserCommand command)
        {
            if (command == null) throw new BadRequestException("Error. Invalid data for disable user");
            var user = await _repository.GetAsync(command.UserId);
            if (user == null) throw new BadRequestException("Error. User to disable not valid");
            user.Disable(command.DisableBy);
            await _repository.UpdateAsync(user);
        }
    }
}
