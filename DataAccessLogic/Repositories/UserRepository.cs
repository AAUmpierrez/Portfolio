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
    public class UserRepository : IUserRepository
    {
        private Context _context { get; set; }


        public UserRepository(Context context) 
        {
            _context = context;
        }

        public async Task AddAsync(User item)
        {
            if (await GetByEmailAsync(item.Email) != null) throw new BussinesException("Error. A user with that email address already exists");
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
        }


        public async Task <User> GetAsync(int id)
        {
            User u = await _context.Users
                                  .Include(u=>u.CreatedTickets)
                                  .Include(u=>u.AssignedTickets)
                                  .Include (u=>u.Comments)
                                  .Include (u =>u.Role)
                                  .IgnoreQueryFilters()
                                  .SingleOrDefaultAsync(u => u.Id==id);
            return u;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Role)
                                       .IgnoreQueryFilters()
                                       .ToListAsync();
        }

        public async Task UpdateAsync(User item)
        {
            _context.Users.Update(item);
            await _context.SaveChangesAsync();
        }

        //Private method

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(u=>u.Role).SingleOrDefaultAsync(u => u.Email == email);            
        }


        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllByStatus(UserStatus uStatus)
        {
            IQueryable<User> query = _context.Users;

            if(uStatus == UserStatus.Inactive)
            {
                query = query.IgnoreQueryFilters().Where(u=> u.Status == UserStatus.Inactive);
            }
            else if(uStatus == UserStatus.All)
            {
                query = query.IgnoreQueryFilters();
            }
            return await query.ToListAsync();
        }


    }
}
