using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.WaitingTicket
{
    public class WaitingTicketCommandHandler:IRequestHandler<WaitingTicketCommand>
    {
        private  readonly ITicketRepository _repository;

        public WaitingTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(WaitingTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException("Ticket not found");
            ticket.Waiting(request.CurrentUserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
