using AplicationLogic.DTOs.User;
using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
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
    public class GetUserQueryHandler:IQueryHandler<GetUserQuery, GetUserDto>
    {
        private IUserRepository _userRepository {  get; set; }

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Execute(GetUserQuery query)
        {
            if (query.Id <= 0) throw new BadRequestException("Error. User not exist");
            var ticket = await _userRepository.GetAsync(query.Id);
            return UserMapper.UserToUserDto(ticket);
        }
    }
}
