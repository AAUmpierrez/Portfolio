using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.Ticket
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int CreatorUser { get; set; }
        public int AssignedUser { get; set; }
        public DateTime ClosingDate { get; set; }
    }
}
