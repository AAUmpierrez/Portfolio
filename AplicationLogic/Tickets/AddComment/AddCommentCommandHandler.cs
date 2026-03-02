using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
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
    public class AddCommentCommandHandler:ICommandHandler<AddCommentCommand>
    {
        private ITicketRepository _repository {  get; set; }
        public AddCommentCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(AddCommentCommand command)
        {
            if (command == null) throw new BadRequestException("Error. Comment data not valid");
            var ticket = await _repository.GetAsync(command.TicketId);
            if (ticket == null) throw new BadRequestException("Error. Ticket not valid");
            ticket.AddComment(command.Content, command.IsInternal);
            await _repository.UpdateAsync(ticket);
        }
    }
}
