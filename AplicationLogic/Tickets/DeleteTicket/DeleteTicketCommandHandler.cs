using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class DeleteTicketCommandHandler:ICommandHandler<DeleteTicketCommand>
    {
        private ITicketRepository _repository {  get; set; }

        public DeleteTicketCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(DeleteTicketCommand tCommand)
        {
            if (tCommand == null) throw new BadRequestException("Command not valid");
            var ticket = await _repository.GetAsync(tCommand.TicketId);
            if (ticket == null) throw new BadRequestException("Error. Incorrect ticket value");
            ticket.SoftDelete(tCommand.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
