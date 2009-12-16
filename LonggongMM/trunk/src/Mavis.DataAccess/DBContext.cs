using System;
using Mavis.Core;
using NHibernate;

namespace Mavis.DataAccess
{
    public class DBContext: IDBContext
    {
        public DBContext(): this(NHibernateSession.DefaultFactoryKey)
        {
            
        }

        public DBContext(string factoryKey)
        {
            FactoryKey = factoryKey;
        }

        private ISession Session
        {
            get
            {
                return NHibernateSession.CurrentFor(FactoryKey);
            }
        }

        #region Implementation of IDBContext

        public IDisposable OpenSession()
        {
            return Session;
        }

        public void CommitChanges()
        {
            Session.Flush();
        }

        public IDisposable BeginTransaction()
        {
            return Session.BeginTransaction();
        }

        public void CommitTransaction()
        {
            Session.Transaction.Commit();
        }

        public void RollbackTransaction()
        {
            Session.Transaction.Rollback();
        }

        #endregion

        public string FactoryKey { get; set; }
    }
}