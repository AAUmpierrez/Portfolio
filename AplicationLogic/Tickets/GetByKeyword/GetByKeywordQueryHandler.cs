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

namespace AplicationLogic.Tickets.GetByKeyword
{
    public class GetByKeywordQueryHandler : IRequestHandler<GetByKeywordQuery, List<TicketListDto>>
    {
        private readonly ITicketRepository _ticketRepository;
        public GetByKeywordQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketListDto>> Handle(GetByKeywordQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAllAsync();            
            var query = tickets.AsQueryable();
            if (string.IsNullOrEmpty(request.Keyword)) throw new BadRequestException("Keyword not valid");
            var result = query.Where(t => t.Title.ToLower().Contains(request.Keyword.ToLower()) 
                                       || t.Description.ToLower().Contains(request.Keyword.ToLower())).ToList();
            if (result.Count == 0) throw new BadRequestException("No tickets found with the given keyword");
            return TicketMapper.TicketsToTicketListDto(result).ToList();
        }
    }
}
