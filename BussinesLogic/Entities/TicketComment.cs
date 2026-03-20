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
        public int UserId { get; set; }
        public User User { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public bool IsInternal { get; set; }
        public DateTime CreatedAt { get; set; }


        private TicketComment() { }

        public TicketComment(int ticketId,int userId, string content, bool isInternal, string role)
        {
            TicketId = ticketId;
            UserId = userId;
            Content = content;
            IsInternal = isInternal;
            CreatedAt = DateTime.Now;
            Role = role;
        }

    }
}
