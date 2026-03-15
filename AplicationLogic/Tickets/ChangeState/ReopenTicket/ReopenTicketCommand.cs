using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.ReopenTicket
{
    public class ReopenTicketCommand:IRequest
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }

    }
}
