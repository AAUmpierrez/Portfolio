using AplicationLogic.DTOs.Ticket;
using AplicationLogic.Interfaces;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Dashboard
{
    public class GetTicketDashboardQueryHandler : IQueryHandler<GetTicketDashboardQuery, TicketDashboardDto>
    {
        private ITicketRepository _ticketRepository { get; set; }

        public GetTicketDashboardQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public Task<TicketDashboardDto> Execute(GetTicketDashboardQuery request)
        {
            var query = _ticketRepository.Query();
            var openTickets =  query.Count(t => t.State == TicketState.Open);
            var resolvedTickets = query.Count(t => t.State == TicketState.Resolved);
            var slaBreached = query.Count(t => t.IsSlaBreached);
            var slaNearToExpire = query.Count(t =>
                t.SlaDueDate > DateTime.UtcNow &&
                t.SlaDueDate < DateTime.UtcNow.AddHours(2));
            var dashboard = new TicketDashboardDto
            {
                OpenTickets = openTickets,
                ResolvedTickets = resolvedTickets,
                SlaBreached = slaBreached,
                SlaNearToExpire = slaNearToExpire
            };

            return Task.FromResult(dashboard);
        } 
    
    }
}
