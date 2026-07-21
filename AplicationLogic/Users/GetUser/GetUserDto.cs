using AplicationLogic.Tickets.ListTicket;
using AplicationLogic.Users.AddComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Users.GetUser
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public string Status { get; set; }
        public IEnumerable<TicketListDto> CreatedTickets { get; set; } = new List<TicketListDto>();
        public IEnumerable<TicketListDto> AssignedTickets { get; set; } = new List<TicketListDto>();
        public IEnumerable<UserCommentDto> Comments{ get; set; } = new List<UserCommentDto>();
    }
}
