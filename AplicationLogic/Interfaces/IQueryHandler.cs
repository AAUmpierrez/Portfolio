using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicationLogic.Interfaces
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> Execute(TQuery query);
    }
}
