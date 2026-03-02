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
        public static Ticket TicketDtoToTicket(TicketDto tDto)
        {
            if (tDto == null) throw new BadRequestException("Error. Incorrect data enter");
            Ticket ticket = new Ticket(tDto.Title, 
                                       tDto.Description,
                                       (TicketPriority)tDto.Priority,
                                       tDto.CreatorUser);
            return ticket;
        }

        public static IEnumerable<TicketListDto> TicketsToTicketsDto (List<Ticket> tickets)
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
    }
}
