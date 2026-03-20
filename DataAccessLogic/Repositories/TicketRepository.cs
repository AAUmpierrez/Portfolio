using BussinesLogic.Entities;
using BussinesLogic.Enums;
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
    public class TicketRepository : ITicketRepository
    {
        private Context _context { get; set; }
        public TicketRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Ticket item)
        {
            _context.Tickets.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync()
        {
            return await _context.Tickets.IgnoreQueryFilters().ToListAsync();
        }

        public IQueryable<Ticket> Query()
        {
            return _context.Tickets.AsQueryable();
        }

        public async Task<Ticket> GetAsync(int id)
        {
            return await _context.Tickets.Include(t => t.Comments).ThenInclude(c=>c.User).ThenInclude(u=>u.Role)
                                        .Include(t => t.CreatorUser)
                                        .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Ticket item)
        {
            _context.Tickets.Update(item);
            await _context.SaveChangesAsync();
        }

    }
}
