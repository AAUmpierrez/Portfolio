using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.ChangeState.AssignTicket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.CloseTicket
{
    public class CloseTicketCommandHandler : ICommandHandler<CloseTicketCommand>
    {
        private ITicketRepository _repository { get; set; }

        public CloseTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(CloseTicketCommand command)
        {
            if (command == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(command.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket {command.TicketId} not found");
            ticket.Close(command.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
