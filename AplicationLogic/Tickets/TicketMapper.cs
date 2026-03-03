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
                                       tCommand.CreatorUser);
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
                CreatorUser = (t.CreatorUser.FirstName + t.CreatorUser.LastName),
                AssignedUser = (t.AssignedUser.FirstName + t.AssignedUser.LastName),
                CreatedDate = t.CreationDate,
                ClosingDate = t.ClosingDate,
            });
        }
        public static Ticket UpdateTicketCommandToUpdateTicket(UpdateTicketCommand tCommand)
        {
            Ticket ticket = new Ticket(tCommand.Title,
                                       tCommand.Description,
                                       (TicketPriority)tCommand.Priority,
                                       tCommand.CreatorUser);
            return ticket;                                  
        }


        public static TicketDto TicketToTicketDto(Ticket ticket)
        {
            return new TicketDto()
            {
                Id = ticket.Id,
                Title = ticket.Title,
                Description = ticket.Description,
                CreatorUser = ticket.CreatorUserId

            };
        }
    }
}
