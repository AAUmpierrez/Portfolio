using MediatR;
using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Ticketinterf
{
    public class AddTicketCommentCommand:IRequest
    {
        public int TicketId { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }
    }
}
