using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class ChangePriorityCommandHandler : IRequestHandler<ChangePriorityCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public ChangePriorityCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(ChangePriorityCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket {request.TicketId} not found");
            ticket.ChangePriority((TicketPriority)request.NewPriority, request.CurrentUser);
            await _repository.UpdateAsync(ticket);
        }
    }
}
