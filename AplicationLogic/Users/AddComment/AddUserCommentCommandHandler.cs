using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class AddUserCommentCommandHandler : IRequestHandler<AddUserCommentCommand>
    {
        private IUserRepository _repository {  get; set; }

        public AddUserCommentCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(AddUserCommentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var user = await _repository.GetAsync(request.UserId);
            if (user == null) throw new NotFoundException($"User {request.UserId} not found");
            user.AddComment(request.Content, request.IsInternal);
            await _repository.UpdateAsync(user);
        }
    }
}
