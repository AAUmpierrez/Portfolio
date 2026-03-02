using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Enums
{
    public enum TicketState
    {
        Open = 1,
        Assigned = 2,
        InProcess = 3,
        Waiting = 4,
        Resolved = 5,
        Close = 6
    }
}
