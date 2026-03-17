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

        public async Task Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var ticket = await _repository.GetAsync(request.TicketId);

            if (ticket == null) throw new BadRequestException("Ticket not found");

            if ((ticket.State == TicketState.Close && ticket.AssignedUserId.HasValue))
                throw new BussinesException("Closed Ticket can not be modify");
            ticket.UpdateDetails(request.Title,request.Description);

            await _repository.UpdateAsync(ticket);
        }
    }
}
