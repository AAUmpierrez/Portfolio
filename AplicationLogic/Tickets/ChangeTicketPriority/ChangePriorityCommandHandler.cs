using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class ChangePriorityCommandHandler : ICommandHandler<ChangePriorityCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public ChangePriorityCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }
        public async Task Execute(ChangePriorityCommand command)
        {
            if (command == null) throw new BadRequestException("Ticket not valid");
            var ticket = await _repository.GetAsync(command.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket {command.TicketId} not found");
            ticket.ChangePriority((TicketPriority)command.NewPriority, command.CurrentUser);
            await _repository.UpdateAsync(ticket);
        }
    }
}
