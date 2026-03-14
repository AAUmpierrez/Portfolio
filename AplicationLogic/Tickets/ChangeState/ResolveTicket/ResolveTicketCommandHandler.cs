using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.ChangeState.AssignTicket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.ResolveTicket
{
    public class ResolveTicketCommandHandler : ICommandHandler<ResolveTicketCommand>
    {
        private ITicketRepository _repository { get; set; }

        public ResolveTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(ResolveTicketCommand command)
        {
            if (command == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(command.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket {command.TicketId} not found");
            ticket.Resolve(command.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
