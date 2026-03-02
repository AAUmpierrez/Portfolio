using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.Ticket
{
    public class DeleteTicketDto
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
    }
}
