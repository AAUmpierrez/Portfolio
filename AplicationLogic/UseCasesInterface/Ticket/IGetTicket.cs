using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.UseCasesInterface.Ticket
{
    public interface IGetTicket
    {
        Task Execute(int id);
    }
}
