using MediatR;
using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.GetByKeyword
{
    public class GetByKeywordQuery:IRequest<List<TicketListDto>>
    {
        public string Keyword { get; set; }
    }
}
