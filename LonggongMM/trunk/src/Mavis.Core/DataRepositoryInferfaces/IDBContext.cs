using System;

namespace Mavis.Core
{
    public interface IDBContext
    {
        IDisposable OpenSession();

        void CommitChanges();
        IDisposable BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}