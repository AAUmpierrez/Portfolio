using MediatR;
using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.CommentList
{
    public class TicketCommentListQuery:IRequest<List<TicketCommentDto>>
    {
        public int TicketId { get; set; }
    }
}
