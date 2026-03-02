using AplicationLogic.UseCasesInterface.User;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.User
{
    public class GetUser:IGetUser
    {
        private IUserRepository _userRepository {  get; set; }

        public GetUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(int id)
        {
            if (id <= 0) throw new BadRequestException("Error. User not exist");
            await _userRepository.GetAsync(id);
        }
    }
}
