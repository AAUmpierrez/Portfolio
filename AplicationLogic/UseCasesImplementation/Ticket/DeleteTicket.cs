using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class DeleteTicket:IDeleteTicket
    {
        private ITicketRepository _repository {  get; set; }

        public DeleteTicket(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(DeleteTicketDto tDto)
        {
            if (tDto == null) throw new BadRequestException("Error. Incorrect data to delet ticket");
            var ticket = await _repository.GetAsync(tDto.TicketId);
            if (ticket == null) throw new BadRequestException("Error. Incorrect ticket value");
            ticket.SoftDelete(tDto.UserId);
            await _repository.UpdateAsync(ticket);
        }
    }
}
