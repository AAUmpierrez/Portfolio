using AplicationLogic.Tickets.ListTicket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.GetMyTickets
{
    public class GetMyTicketsQuery:IRequest<List<GetMyTicketDto>>
    {
        public int? Status { get; set; }
        public int? Priority { get; set; }
        public int CurrentUserId { get; set; }
    }
}
