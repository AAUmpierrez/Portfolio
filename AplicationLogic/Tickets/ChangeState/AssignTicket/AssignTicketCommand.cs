using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.AssignTicket
{
    public class AssignTicketCommand:IRequest
    {
        public int TicketId { get; set; }
        public int AssignedUserId { get; set; }
        public int AssignedByUserId { get; set; }
    }
}
