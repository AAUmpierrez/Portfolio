using AplicationLogic.DTOs.Ticket;
using AplicationLogic.Interfaces;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AplicationLogic.Tickets.Dashboard
{
    public class GetTicketDashboardQueryHandler : IRequestHandler<GetTicketDashboardQuery, TicketDashboardDto>
    {
        private ITicketRepository _ticketRepository { get; set; }

        public GetTicketDashboardQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task<TicketDashboardDto> Handle(GetTicketDashboardQuery request, CancellationToken cancellationToken)
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

            return await Task.FromResult(dashboard);
            //return dashboard;
        }
    }
}
