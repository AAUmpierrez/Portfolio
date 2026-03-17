using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
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
    public class AddTicketCommentCommandHandler:IRequestHandler<AddTicketCommentCommand>
    {
        private ITicketRepository _repository {  get; set; }
        public AddTicketCommentCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(AddTicketCommentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Comment data not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new BadRequestException("Error. Ticket not valid");
            ticket.AddComment(request.Content, request.IsInternal);
            await _repository.UpdateAsync(ticket);
        }
    }
}
