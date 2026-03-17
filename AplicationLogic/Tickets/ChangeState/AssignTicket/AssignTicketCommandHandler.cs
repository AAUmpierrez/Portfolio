using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.AssignTicket
{
    public class AssignTicketCommandHandler:IRequestHandler<AssignTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public AssignTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(AssignTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket{request.TicketId} not found");            
            ticket.Assigned(request.AssignedByUserId,request.AssignedUserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
