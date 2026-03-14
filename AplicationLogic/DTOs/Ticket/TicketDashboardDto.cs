using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.DTOs.Ticket
{
    public class TicketDashboardDto
    {
        public int OpenTickets { get; set; }
        public int ResolvedTickets { get; set; }
        public int SlaBreached { get; set; }
        public int SlaNearToExpire { get; set; }
    }
}
