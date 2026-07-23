using BussinesLogic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLogic.RepositoryInterfaces
{
    public interface ITicketCommentRepository:IRepository<TicketComment>
    {
        public void Delete(TicketComment comment);
    }
}
