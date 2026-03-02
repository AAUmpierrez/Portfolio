using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Ticketinterf
{
    public class DeleteTicketCommand
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
