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

namespace AplicationLogic.Tickets.CommentList
{
    public class TicketCommentListQueryHandler : IRequestHandler<TicketCommentListQuery, List<TicketCommentDto>>
    {
        private readonly ITicketRepository _repository;

        public TicketCommentListQueryHandler(ITicketRepository repository)
        {
            _repository = repository;
        }


        public async Task<List<TicketCommentDto>> Handle(TicketCommentListQuery request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Query not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if(ticket == null) throw new NotFoundException($"Ticket with id {request.TicketId} not found");
            var comments = TicketMapper.CommentsToCommentDto(ticket.Comments);
            comments.OrderBy(c => c.CreatedAt);
            return comments.ToList();
        }
    }
}
