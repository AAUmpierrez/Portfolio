using AplicationLogic.DTOs.User;
using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using SharedLogic.Mappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class GetUserQueryHandler:IRequestHandler<GetUserQuery,GetUserDto>
    {
        private IUserRepository _userRepository {  get; set; }

        public GetUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0) throw new BadRequestException("Error. User not exist");
            var user = await _userRepository.GetAsync(request.Id);
            if (user == null) throw new NotFoundException("Error. User not found");
            return UserMapper.UserToUserDto(user);
        }

    }
}
