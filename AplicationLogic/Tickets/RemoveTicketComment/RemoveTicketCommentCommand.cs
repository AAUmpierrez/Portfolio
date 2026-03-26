using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.RemoveTicketComment
{
    public class RemoveTicketCommentCommand:IRequest
    {
        public int TicketId { get; set; }
        public int TicketAttachmentId { get; set; }
        public int CommentId { get; set; }
    }
}
