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
using MediatR;

namespace AplicationLogic.UseCasesInterface.User
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
    {
        private IUserRepository _repository { get; set; }

        public AddUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }


        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Error. The entered data is incorrect. ");
            var user = UserMapper.AddUserComandToUser(request);
            await _repository.AddAsync(user);
            return user.Id;
        }
    }
}
