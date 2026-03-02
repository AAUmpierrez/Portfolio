using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.Enums;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class ChangePriority : IChangePriority
    {
        private ITicketRepository _repository {  get; set; }

        public ChangePriority(ITicketRepository repository)
        {
            _repository = repository;
        }
        public async Task Execute(ChangePriorityTicketDto tDto)
        {
            var ticket = await _repository.GetAsync(tDto.TicketId);
            if (ticket == null) throw new NotFoundException("Error. Ticket not found");
            ticket.ChangePriority((TicketPriority)tDto.NewPriority,tDto.CurrentUser);
            await _repository.UpdateAsync(ticket);
        }
    }
}
