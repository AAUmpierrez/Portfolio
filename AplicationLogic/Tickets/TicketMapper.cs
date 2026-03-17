using AplicationLogic.Tickets.Ticketinterf;
using BussinesLogic.Entities;
using BussinesLogic.Enums;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.Mappers
{
    public class TicketMapper
    {
        public static Ticket AddTicketCommandToTicket(AddTicketCommand tCommand)
        {
            Ticket ticket = new Ticket(tCommand.Title,
                                       tCommand.Description,
                                       (TicketPriority)tCommand.Priority,
                                       tCommand.CreatorUser,
                                       tCommand.SlaDueDate);
            return ticket;
        }

        public static IEnumerable<TicketListDto> TicketsToTicketListDto (List<Ticket> tickets)
        {
            return tickets.Select(t => new TicketListDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Priority = t.Priority.ToString(),
                State = t.State.ToString(),
                CreatorUserId = t.CreatorUserId,
                CreatedDate = t.CreationDate,
                SlaDueDate = t.SlaDueDate,
                IsSlaBreached = t.IsSlaBreached,
                TimeRemainig = t.SlaDueDate - DateTime.Now,
            });
        }

        public static IEnumerable<TicketCommentDto> CommentsToCommentDto(IEnumerable<TicketComment> comments)
        {
            return comments.Select(c => new TicketCommentDto()
            {
                TicketId = c.Id,
                Content = c.Content,
                IsInternal = c.IsInternal,
            });
        }

        public static TicketDto TicketToTicketDto(Ticket ticket)
        {
            return new TicketDto()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CreatorUser = ticket.CreatorUserId,
                Priority = ticket.Priority,
                State = ticket.State
            };
        }

      
    }
}
