using BussinesLogic.Enums;
using MediatR;
using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Ticketinterf
{
    public class GetAllTicketQuery:IRequest<IEnumerable<TicketListDto>>
    {
        public int Priority { get; set; }

        public int Status { get; set; }

        public bool? IsSlaBreached { get; set; }

        public bool OrderBySlaDueDate { get; set; }
    }
}
