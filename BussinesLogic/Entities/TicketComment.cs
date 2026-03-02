using BussinesLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class TicketComment
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }
        public DateTime CreatedAt { get; set; }


        public TicketComment() { }

        public TicketComment(int ticketId,string content,bool isInternal)
        {
            TicketId = ticketId;
            Content = content;
            IsInternal = isInternal;
            CreatedAt = DateTime.Now;
            Validate();
        }

        private void Validate()
        {
            if (TicketId <= 0) throw new CommentException("Error. Ticket can not be Empty");
            if (string.IsNullOrEmpty(Content)) throw new CommentException("Error. Comment must have content");
        }

    }
}
