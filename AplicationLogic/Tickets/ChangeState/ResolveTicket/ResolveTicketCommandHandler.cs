using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.ChangeState.AssignTicket;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.ResolveTicket
{
    public class ResolveTicketCommandHandler : IRequestHandler<ResolveTicketCommand>
    {
        private ITicketRepository _repository { get; set; }

        public ResolveTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }


        public async Task Handle(ResolveTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket {request.TicketId} not found");
            ticket.Resolve(request.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
