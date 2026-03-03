using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Interfaces
{
    public interface ICommandHandler<TCommand>
    {
        Task Execute(TCommand command);
    }
}
