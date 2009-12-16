using System;
using System.Collections.Generic;
using System.Reflection;
using Mavis.Core;
using NHibernate;
using NHibernate.Criterion;

namespace Mavis.DataAccess
{
    public class NHibernateRepository<T>: NHibernateRepositoryWithTypedId<T, int>, INHibernateRepository<T>
    {
    }

    public class NHibernateRepositoryWithTypedId<T, TId> : RepositoryWithTypedId<T, TId>, INHibernateRepositoryWithTypedId<T, TId>
    {
        #region Implementation of INHibernateRepositoryWithTypedId<T,TId>

        public T Get(TId id, CustomEnums.LockMode lockMode)
        {
            return Session.Get<T>(id, ConvertFrom(lockMode));
        }

        public T Load(TId id)
        {
            return Session.Load<T>(id);
        }

        public T Load(TId id, CustomEnums.LockMode lockMode)
        {
            return Session.Load<T>(id, ConvertFrom(lockMode));
        }

        public IList<T> FindAll(T exampleInstance, params string[] propertiesToExclude)
        {
            ICriteria criteria = Session.CreateCriteria(typeof (T));
            var example = Example.Create(exampleInstance);

            foreach (var propertyToExclude in propertiesToExclude)
            {
                example.ExcludeProperty(propertyToExclude);
            }
            criteria.Add(example);

            return criteria.List<T>();
        }

        public T FindOne(T exampleInstance, params string[] propertiesToExclude)
        {
            var foundList = FindAll(exampleInstance, propertiesToExclude);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }

            return (foundList.Count == 1) ? foundList[0] : default(T);
        }

        public T Save(T entity)
        {
            Session.Save(entity);
            return entity;
        }

        public T Update(T entity)
        {
            Session.Update(entity);
            return entity;
        }

        public void Evict(T entity)
        {
            Session.Evict(entity);
        }

        #endregion

        private static LockMode ConvertFrom(CustomEnums.LockMode lockMode)
        {
            FieldInfo translatedLockMode = typeof(LockMode).GetField(lockMode.ToString(),
                                                                     BindingFlags.Public | BindingFlags.Static);

            Check.Ensure(translatedLockMode != null, "The provided lock mode , '" + lockMode + ",' " +
                                                     "could not be translated into an NHibernate.LockMode. This is probably because " +
                                                     "NHibernate was updated and now has different lock modes which are out of synch " +
                                                     "with the lock modes maintained in the domain layer.");

            return (LockMode)translatedLockMode.GetValue(null);
        }
    }
}