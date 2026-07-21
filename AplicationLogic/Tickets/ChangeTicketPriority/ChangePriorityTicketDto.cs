using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.ChangeTicketPriority
{
    public class ChangePriorityTicketDto
    {
        public int TicketId { get; set; }
        public int NewPriority { get; set; }
    }
}
