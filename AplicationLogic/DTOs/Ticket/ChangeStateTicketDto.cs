using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.Ticket
{
    public class ChangeStateTicketDto
    {
        public int TicketId { get; set; }
        public int State { get; set; }
        public int CurrentUser { get; set; }

    }
}
