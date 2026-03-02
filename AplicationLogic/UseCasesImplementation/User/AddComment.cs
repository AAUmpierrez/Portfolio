using AplicationLogic.UseCasesInterface.User;
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
    public class AddComment : IAddComment
    {
        private IUserRepository _repository {  get; set; }

        public AddComment(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task Excecute(UserCommentDto uDto)
        {
            if (uDto == null) throw new BadRequestException("Error. Comment data not valid");
            var user = await _repository.GetAsync(uDto.UserId);
            if (user == null) throw new BadRequestException("Error. User not valid");
            user.AddComment(uDto.Content, uDto.IsInternal);
            await _repository.UpdateAsync(user);
        }
    }
}
