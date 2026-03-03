using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeState.CloseTicket
{
    public class CloseTicketCommand
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
