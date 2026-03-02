using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.Entities
{
    public class TicketHistory
    {
        public int Id { get; private set; }

        public int TicketId { get; private set; }
        public Ticket Ticket { get; private set; }

        public int ChangedByUserId { get; private set; }
        public User ChangedByUser { get; private set; }

        public string PropertyChanged { get; private set; } 
        public string OldValue { get; private set; }
        public string NewValue { get; private set; }

        public DateTime ChangedAt { get; private set; }

        private TicketHistory() { }

        public TicketHistory(
            int ticketId,
            int changedByUserId,
            string propertyChanged,
            string oldValue,
            string newValue)
        {
            TicketId = ticketId;
            ChangedByUserId = changedByUserId;
            PropertyChanged = propertyChanged;
            OldValue = oldValue;
            NewValue = newValue;
            ChangedAt = DateTime.UtcNow;
        }
    }
}
