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
    public class GetAllTicketsQueryHandler:IQueryHandler<GetAllTicketQuery,IEnumerable<TicketListDto>>
    {
        private ITicketRepository _repository {  get; set; }

        public GetAllTicketsQueryHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TicketListDto>> Execute(GetAllTicketQuery query)
        {
            if (query == null) throw new BadRequestException("Query is not valid");
            var tickets = await _repository.GetAllAsync();

            return TicketMapper.TicketsToTicketListDto(tickets.ToList());
        }

    }
}
