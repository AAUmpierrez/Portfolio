using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class AddComment:IAddComment
    {
        private ITicketRepository _repository {  get; set; }
        public AddComment(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Excecute(TicketCommentDto tDto)
        {
            if (tDto == null) throw new BadRequestException("Error. Comment data not valid");
            var ticket = await _repository.GetAsync(tDto.TicketId);
            if (ticket == null) throw new BadRequestException("Error. Ticket not valid");
            ticket.AddComment(tDto.Content, tDto.IsInternal);
            await _repository.UpdateAsync(ticket);
        }
    }
}
