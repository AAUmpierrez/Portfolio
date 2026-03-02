using SharedLogic.DTOs.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.Ticket
{
    public interface IDeleteTicket
    {
        Task Execute(DeleteTicketDto tDto);
    }
}
