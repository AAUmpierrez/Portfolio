using AplicationLogic.UseCasesInterface.Ticket;
using BussinesLogic.RepositoryInterfaces;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesImplementation.Ticket
{
    public class GetTicket:IGetTicket
    {
        private ITicketRepository _repository {  get; set; }

        public GetTicket(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task Execute(int id)
        {
            if (id <= 0) throw new BadRequestException("Error. Incorrect ticket id");
            await _repository.GetAsync(id);
        }
    }
}
