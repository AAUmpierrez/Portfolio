using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.Entities;
using BussinesLogic.Enums;
using BussinesLogic.Exceptions;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class ChangeTicketState:IChangeTicketState
    {
        private ITicketRepository _repository { get; set; }

        public ChangeTicketState(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Excecute(ChangeStateTicketDto tDto)
        {
            var ticket = await _repository.GetAsync(tDto.TicketId);
            var actions = new Dictionary<TicketState, Action>()
            {
                { TicketState.Assigned, () => ticket.Assigned(tDto.CurrentUser) },
                { TicketState.InProcess, () => ticket.InProgress() },
                { TicketState.Waiting, () => ticket.Waiting() },
                { TicketState.Resolved, () => ticket.Resolve(tDto.CurrentUser) },
                { TicketState.Close, () => ticket.Close(tDto.CurrentUser) },
                { TicketState.Open, () => ticket.Reopen(tDto.CurrentUser) }
            };
            if (!actions.ContainsKey((TicketState)tDto.State))
                throw new TicketException("State transition not supported");

            actions[(TicketState)tDto.State].Invoke();

            await _repository.UpdateAsync(ticket);
        }
    }
}
