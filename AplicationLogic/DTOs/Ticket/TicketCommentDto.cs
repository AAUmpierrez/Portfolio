using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.Ticket
{
    public class TicketCommentDto
    {
        public int TicketId { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }

    }
}
