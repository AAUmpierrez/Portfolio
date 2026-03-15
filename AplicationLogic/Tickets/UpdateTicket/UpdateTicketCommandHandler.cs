using AplicationLogic.Interfaces;
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

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class UpdateTicketCommandHandler:IRequestHandler<UpdateTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public UpdateTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(UpdateTicketCommand tCommand)
        {
        }

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            if ((TicketState)request.State == TicketState.Close && request.AssignedUserId.HasValue)
                throw new BussinesException("Closed Ticket can not be modify");


            await _repository.UpdateAsync(TicketMapper.UpdateTicketCommandToUpdateTicket(request));
        }
    }
}
