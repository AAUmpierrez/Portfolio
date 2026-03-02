using AplicationLogic.UseCasesInterface.User;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.User
{
    public class DisableUser:IDisableUser
    {
        private IUserRepository _repository {  get; set; }

        public DisableUser(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(DisableUserDto uDto)
        {
            if (uDto == null) throw new BadRequestException("Error. Invalid data for disable user");
            var user = await _repository.GetAsync(uDto.UserId);
            if (user == null) throw new BadRequestException("Error. User to disable not valid");
            user.Disable(uDto.DisableBy);
            await _repository.UpdateAsync(user);
        }
    }
}
