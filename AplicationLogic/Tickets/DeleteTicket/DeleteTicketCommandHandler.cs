using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class DeleteTicketCommandHandler:IRequestHandler<DeleteTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public DeleteTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }


        public async Task Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new NotFoundException($"Ticket not found");
            if (ticket.State != TicketState.Close) throw new BussinesException("Only close ticket can be deleted");
            ticket.SoftDelete(request.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
