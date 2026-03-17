using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
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
    public class GetTicketQueryHandler:IRequestHandler<GetTicketQuery,TicketDto>
    {
        private ITicketRepository _repository {  get; set; }

        public GetTicketQueryHandler(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<TicketDto> Handle(GetTicketQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0) throw new BadRequestException("Ticket not valid");
            var ticket =  await _repository.GetAsync(request.Id);
            if (ticket == null) throw new NotFoundException($"Ticket {request.Id} not found");
            TicketDto dto = TicketMapper.TicketToTicketDto(ticket);
            dto.Comments = TicketMapper.CommentsToCommentDto(ticket.Comments);
            return dto;
        }
    }
}
