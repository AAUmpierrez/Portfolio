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

namespace AplicationLogic.Tickets.ChangeState.ReopenTicket
{
    public class ReopenTicketCommandHandler : IRequestHandler<ReopenTicketCommand>
    {
        private ITicketRepository _repository { get; set; }

        public ReopenTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(ReopenTicketCommand command)
        {
        }

        public async Task Handle(ReopenTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket {request.TicketId} not found");
            ticket.Reopen(request.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
