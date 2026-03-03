using SharedLogic.DTOs.User;
using BussinesLogic.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLogic.Exceptions;
using SharedLogic.Mappers;
using AplicationLogic.Interfaces;

namespace AplicationLogic.UseCasesInterface.User
{
    public class AddUserCommandHandler : ICommandHandler<AddUserCommand>
    {
        private IUserRepository _repository { get; set; }

        public AddUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task Execute(AddUserCommand command)
        {
            if (command == null) throw new BadRequestException("Error. The entered data is incorrect. ");
            await _repository.AddAsync(UserMapper.AddUserComandToUser(command));
        }
    }
}
