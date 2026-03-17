using MediatR;
using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Ticketinterf
{
    public class UpdateTicketCommand:IRequest
    {
        public int TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
