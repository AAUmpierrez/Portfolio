using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Enums;
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
    public class GetAllTicketsQueryHandler:IQueryHandler<GetAllTicketQuery,IEnumerable<TicketListDto>>
    {
        private ITicketRepository _repository {  get; set; }

        public GetAllTicketsQueryHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TicketListDto>> Execute(GetAllTicketQuery request)
        {
            var tickets = await _repository.GetAllAsync();


            var query = tickets.AsQueryable();

            if (request.Priority > 0)
            {
                query = query.Where(t => t.Priority == (TicketPriority)request.Priority);
            }

            if (request.Status > 0)
            {
                query = query.Where(t => t.State == (TicketState)request.Status);
            }

            if (request.IsSlaBreached.HasValue)
            {
                query = query.Where(t => t.IsSlaBreached == request.IsSlaBreached);
            }

            if (request.OrderBySlaDueDate)
            {
                query = query.OrderBy(t => t.SlaDueDate);
            }


            return TicketMapper.TicketsToTicketListDto(tickets.ToList());
        }

    }
}
