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
        public string State { get; set; }
        public int CreatorUserId { get; set; }
        public bool IsSlaBreached { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime SlaDueDate { get; set; }
        public TimeSpan TimeRemainig {get; set; }
    }
        
}
