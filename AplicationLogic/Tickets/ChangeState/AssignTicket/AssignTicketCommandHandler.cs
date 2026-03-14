using AplicationLogic.Interfaces;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.AssignTicket
{
    public class AssignTicketCommandHandler:ICommandHandler<AssignTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public AssignTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(AssignTicketCommand command)
        {
            if (command == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(command.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket{command.TicketId} not found");
            ticket.Assigned(command.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
