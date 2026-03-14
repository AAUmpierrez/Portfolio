using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using SharedLogic.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class AddTicketCommandHandler : ICommandHandler<AddTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public AddTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(AddTicketCommand tCommand)
        {
            if (tCommand == null) throw new BadRequestException("Ticket not valid");
            await _repository.AddAsync(TicketMapper.AddTicketCommandToTicket(tCommand));
        }
    }
}
