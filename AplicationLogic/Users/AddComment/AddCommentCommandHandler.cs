using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.User
{
    public class AddCommentCommandHandler : ICommandHandler<AddCommentCommand>
    {
        private IUserRepository _repository {  get; set; }

        public AddCommentCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task Execute(AddCommentCommand command)
        {
            if (command == null) throw new BadRequestException("Error. Comment data not valid");
            var user = await _repository.GetAsync(command.UserId);
            if (user == null) throw new BadRequestException("Error. User not valid");
            user.AddComment(command.Content, command.IsInternal);
            await _repository.UpdateAsync(user);
        }


    }
}
