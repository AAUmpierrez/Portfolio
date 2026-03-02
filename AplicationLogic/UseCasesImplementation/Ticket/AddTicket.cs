using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.DTOs.Ticket;
using SharedLogic.Exceptions;
using SharedLogic.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class AddTicket : IAddTicket
    {
        private ITicketRepository _repository {  get; set; }

        public AddTicket(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(TicketDto tDto)
        {
            if (tDto == null) throw new BadRequestException("Error. Incorrect ticket data enter");
            await _repository.AddAsync(TicketMapper.TicketDtoToTicket(tDto));
        }
    }
}
