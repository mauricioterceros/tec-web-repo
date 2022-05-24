using System;
using System.Collections.Generic;
using System.Text;

namespace DBLayer
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
        
        // FROM HERE
        void Save();

        UserRepository UserRepository { get; }
    }
}
