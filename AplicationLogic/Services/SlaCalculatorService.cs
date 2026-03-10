using BussinesLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Services
{
    public class SlaCalculatorService
    {
        public DateTime CalculateDueDate(TicketPriority priority, DateTime createdAt)
        {
            int hours = priority switch
            {
                TicketPriority.Low => 72,
                TicketPriority.Medium => 48,
                TicketPriority.High => 24,
                TicketPriority.Critical => 4,
                _ => 48
            };

            return createdAt.AddHours(hours);
        }
    }
}
