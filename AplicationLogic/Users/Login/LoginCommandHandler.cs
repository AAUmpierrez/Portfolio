using AplicationLogic.DTOs.User;
using AplicationLogic.Interfaces.Security;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;

        public LoginCommandHandler(IUserRepository userRepository,
                                   IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null)
                throw new Exception("Error. Incorrect password or email");

            if (user.Password != request.Password)
                throw new Exception("Error. Incorrect password or email");

            var token = _jwtService.GenerateToken(new LoginResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role.Name
            });

            return new LoginResponseDto
            {
                Token = token
            };
        }
    }
}
