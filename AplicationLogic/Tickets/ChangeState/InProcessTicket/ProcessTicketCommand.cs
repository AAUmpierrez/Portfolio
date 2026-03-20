using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.InProcessTicket
{
    public class ProcessTicketCommand:IRequest
    {
        public int TicketId { get; set; }
        public int CurrentUserId { get; set; }
    }
}
