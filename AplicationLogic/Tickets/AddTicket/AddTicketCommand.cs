using MediatR;
using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Ticketinterf
{
    public class AddTicketCommand:IRequest<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int State { get; set; }
        public int CreatorUser { get; set; }
        public DateTime SlaDueDate { get; set; }

    }
}
