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

namespace AplicationLogic.Tickets.GetMyTickets
{
    public class GetMyTicketsQueryHandler:IRequestHandler<GetMyTicketsQuery,List<TicketListDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        public GetMyTicketsQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<List<TicketListDto>> Handle(GetMyTicketsQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Query not valid");
            var tickets = await _ticketRepository.GetAllAsync();
            var query = tickets.AsQueryable();
            query = query.Where(t => t.CreatorUserId == request.CurrentUserId);

            if (request.Status.HasValue)
            {
                query = query.Where(t => t.State == (TicketState)request.Status);
            }
            if (request.Priority.HasValue)
            {
                query = query.Where(t => t.Priority == (TicketPriority)request.Priority);
            }

            return TicketMapper.TicketsToTicketListDto(query.ToList()).ToList();
        }
    }
}
