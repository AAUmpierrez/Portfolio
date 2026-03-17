using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.WaitingTicket
{
    public class WaitingTicketCommand:IRequest
    {
        public int TicketId { get; set; }
        public string Comment { get; set; }
    }
}
