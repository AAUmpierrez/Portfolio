using AplicationLogic.Interfaces;
using AplicationLogic.Services;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Enums;
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
        private SlaCalculatorService _slaCalculator { get; set; }

        public AddTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(AddTicketCommand tCommand)
        {
            if (tCommand == null) throw new BadRequestException("Error. Incorrect ticket data enter");
            var createdAt = DateTime.Now;
            var slaDueDate = _slaCalculator.CalculateDueDate((TicketPriority)tCommand.Priority, createdAt);
            tCommand.SlaDueDate = slaDueDate;
            await _repository.AddAsync(TicketMapper.AddTicketCommandToTicket(tCommand));
        }
    }
}
