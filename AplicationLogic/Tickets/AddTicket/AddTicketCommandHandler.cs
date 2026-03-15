using AplicationLogic.Interfaces;
using AplicationLogic.Services;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
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
    public class AddTicketCommandHandler : IRequestHandler<AddTicketCommand,int>
    {
        private ITicketRepository _repository {  get; set; }
        private SlaCalculatorService _slaCalculator { get; set; }

        public AddTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }


        public async Task<int> Handle(AddTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var createdAt = DateTime.Now;
            var slaDueDate = _slaCalculator.CalculateDueDate((TicketPriority)request.Priority, createdAt);
            request.SlaDueDate = slaDueDate;
            await _repository.AddAsync(TicketMapper.AddTicketCommandToTicket(request));
            return request.Id;
        }
    }
}
