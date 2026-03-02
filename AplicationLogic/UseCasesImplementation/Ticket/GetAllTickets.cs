using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class GetAllTickets:IGetAllTicket
    {
        private ITicketRepository _repository {  get; set; }

        public GetAllTickets(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TicketListDto>> Execute()
        {
            var tickets = await _repository.GetAllAsync();
            return TicketMapper.TicketsToTicketsDto(tickets.ToList());
        }
    }
}
