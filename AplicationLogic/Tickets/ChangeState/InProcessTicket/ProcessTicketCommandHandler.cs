using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.InProcessTicket
{
    public class ProcessTicketCommandHandler:IRequestHandler<ProcessTicketCommand>
    {
        private readonly ITicketRepository _repository;

        public ProcessTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ProcessTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException("Ticket not found");
            ticket.InProcess();
            await _repository.UpdateAsync(ticket);
        }
    }
}
