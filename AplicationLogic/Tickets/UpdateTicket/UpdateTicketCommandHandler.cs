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

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class UpdateTicketCommandHandler:ICommandHandler<UpdateTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public UpdateTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(UpdateTicketCommand tCommand)
        {
            if (tCommand == null) throw new BadRequestException("Command not valid");
            await _repository.UpdateAsync(TicketMapper.UpdateTicketCommandToUpdateTicket(tCommand));
        }
    }
}
