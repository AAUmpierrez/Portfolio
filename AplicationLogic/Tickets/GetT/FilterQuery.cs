using BussinesLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Filter
{
    public class FilterQuery
    {
        public TicketPriority? Priority { get; set; }

        public TicketState? Staste { get; set; }

        public bool? IsSlaBreached { get; set; }

        public bool OrderBySlaDueDate { get; set; }
    }
}
