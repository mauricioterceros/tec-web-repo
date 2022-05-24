using System;
using System.Collections.Generic;
using System.Text;

namespace DBLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private P2DbContext _context;

        private UserRepository _userRepository;
        private AddresssRepository _addresssRepository;

        public UserRepository UserRepository
        {
            get 
            {
                return _userRepository;
            }
        }

        public AddresssRepository AddresssRepository
        {
            get
            {
                return _addresssRepository;
            }
        }

        public UnitOfWork(P2DbContext context)
        {
            _context = context;
            _userRepository = new UserRepository(_context);
            _addresssRepository = new AddresssRepository(_context);
        }
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollBackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public void Save()
        {
            try
            {
                BeginTransaction();
                _context.SaveChanges();
                CommitTransaction();
            }
            catch (Exception ex)
            {
                RollBackTransaction();
                throw;
            }
        }
    }
}
