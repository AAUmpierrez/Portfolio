using AplicationLogic.UseCasesInterface.User;
using SharedLogic.DTOs.User;
using BussinesLogic.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedLogic.Exceptions;
using SharedLogic.Mappers;

namespace AplicationLogic.UseCasesImplementation.User
{
    public class AddUser : IAddUser
    {
        private IUserRepository _repository { get; set; }

        public AddUser(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task Execute(UserDto uDto)
        {
            if (uDto == null) throw new BadRequestException("Error. The entered data is incorrect. ");
            await _repository.AddAsync(UserMapper.UserDtoToUser(uDto));
        }
    }
}
