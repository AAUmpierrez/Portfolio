using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.RemoveTicketComment
{
    public class RemoveTicketCommentCommandHandler : IRequestHandler<RemoveTicketCommentCommand>
    {
        private readonly ITicketRepository _ticketRepository;

        public RemoveTicketCommentCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(RemoveTicketCommentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Command not valid");
            var ticket = await _ticketRepository.GetAsync(request.TicketId);
            if (ticket == null) throw new BadRequestException("Ticket not found");
            var comment = ticket.Comments.FirstOrDefault(c => c.Id == request.CommentId);
            if (comment == null) throw new BadRequestException("Comment not found");
            foreach (var atts in comment.Attachments)
            {
                if (System.IO.File.Exists(atts.FilePath))
                {
                    System.IO.File.Delete(atts.FilePath);
                }
            }            
            ticket.Comments.Remove(comment);
            _ticketRepository.UpdateAsync(ticket);
        }
    }
}
