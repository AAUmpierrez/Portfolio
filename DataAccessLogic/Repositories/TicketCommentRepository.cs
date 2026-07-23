using BussinesLogic.Entities;
using BussinesLogic.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using SharedLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLogic.Repositories
{
    public class TicketCommentRepository : ITicketCommentRepository
    {

        private readonly Context _context;

        public TicketCommentRepository(Context context)
        {
            _context = context;
        }
        public Task AddAsync(TicketComment item)
        {
            throw new NotImplementedException();
        }

        public void Delete(TicketComment comment)
        {
            _context.TicketComments.Remove(comment);
            _context.SaveChanges();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TicketComment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TicketComment> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TicketComment item)
        {
            throw new NotImplementedException();
        }
    }
}
