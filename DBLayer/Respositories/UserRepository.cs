using DBLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DBLayer
{
    public class UserRepository
    {
        private P2DbContext _context;

        public UserRepository(P2DbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public User CreateUser(User user)
        {
            _context.Set<User>().Add(user);
            return user;
        }

        public User GetById(Guid id) 
        {
            return _context.Set<User>().Find(id);
        }

        public User UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }

        public User DeleteUser(User user)
        {
            _context.Set<User>().Remove(user);
            return user;
        }
    }
}