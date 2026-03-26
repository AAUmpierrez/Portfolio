using AplicationLogic.Interfaces;
using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Entities;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using SharedLogic.DTOs.Ticket;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class AddTicketCommentCommandHandler:IRequestHandler<AddTicketCommentCommand>
    {
        private ITicketRepository _repository {  get; set; }
        public AddTicketCommentCommandHandler(ITicketRepository repository)
        {
            _repository = repository;
        }
        public async Task Handle(AddTicketCommentCommand request, CancellationToken cancellationToken)
        {
            if (request == null) throw new BadRequestException("Comment data not valid");
            var ticket = await _repository.GetAsync(request.TicketId);
            if (ticket == null) throw new BadRequestException("Error. Ticket not valid");

            var comment = new TicketComment(ticket.Id, request.CurrentUserId, request.Role, request.IsInternal, request.Content);


            if (request.File != null)
            {
                var uploadsFolder = Path.Combine("wwwroot", "uploads", "tickets");
                var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
                var path = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream);
                }
                var relativePath = Path.Combine("uploads", "tickets", fileName).Replace("\\", "/");
                var attachment = new TicketAttachment(
                    comment.Id,
                    fileName,
                    "/"+ relativePath,
                    request.File.Length,
                    request.File.ContentType,
                    comment.UserId
                );
                comment.AddAttachment(attachment);
            }



            ticket.AddComment(comment);
            await _repository.UpdateAsync(ticket);
        }
    }
}
