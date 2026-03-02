using BussinesLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.Ticket
{
    public class TicketListDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public string CreatorUser { get; set; }
        public string AssignedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosingDate { get; set; }
    }
        
}
