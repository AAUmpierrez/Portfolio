using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLogic.DTOs.Ticket
{
    public class TicketCommentDto
    {
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Role { get; set; }
        public bool IsInternal { get; set; }
        public DateTime CreatedAt { get; set; }
        public IFormFile ?Attachemnt { get; set; }

    }
}
