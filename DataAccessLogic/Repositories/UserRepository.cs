using BussinesLogic.Entities;
using BussinesLogic.Enums;
using BussinesLogic.Exceptions;
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
            if(item != null)
            {
                if (await GetByEmail(item.Email) != null) throw new ConflictException("Error. A user with that email address already exists");
                await _context.Users.AddAsync(item);
                await _context.SaveChangesAsync();
                
            }else throw new UserException("Error. User can not be added");
        }


        public async Task <User> GetAsync(int id)
        {
            if (id <= 0) throw new BadRequestException("Error. Incorrect user");
            User u = await _context.Users
                                  .Include(u=>u.CreatedTickets)
                                  .Include(u=>u.AssignedTickets)
                                  .Include (u=>u.Comments)
                                  .SingleOrDefaultAsync(u => u.Id==id);
            return u;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateAsync(User item)
        {
            if (item != null)
            {
                _context.Users.Update(item);
                await _context.SaveChangesAsync();
            }
            else throw new UserException("Error. User can not be updated");
        }

        //Private method

        private async Task<User> GetByEmail(string email)
        {
            if (email == string.Empty) throw new UserException("Error. Please enter a user mail to serch");
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);            
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
