using BussinesLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.DTOs.Ticket
{
    public class AddTicketDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
    }
}
