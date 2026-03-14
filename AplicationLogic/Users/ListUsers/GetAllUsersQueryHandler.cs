using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
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
    public class GetAllUsersQueryHandler :IRequestHandler<GetAllUsersQuery,IEnumerable<UserListDto>>
    {
        private IUserRepository _userRepository {  get; set; }

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<IEnumerable<UserListDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Query not valid");
            var users = await _userRepository.GetAllAsync();
            return UserMapper.UsersToUsersDto(users.ToList());
        }
    }
}
