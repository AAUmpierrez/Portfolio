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
    public class GetTicketQueryHandler:IQueryHandler<GetTicketQuery,TicketDto>
    {
        private ITicketRepository _repository {  get; set; }

        public GetTicketQueryHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketDto> Execute(GetTicketQuery tQuery)
        {
            if (tQuery.Id <= 0) throw new BadRequestException("Ticket not valid");
            var ticket =  await _repository.GetAsync(tQuery.Id);
            if (ticket == null) throw new NotFoundException($"Ticket {tQuery.Id} not found");
            return TicketMapper.TicketToTicketDto(ticket);
        }
    }
}
