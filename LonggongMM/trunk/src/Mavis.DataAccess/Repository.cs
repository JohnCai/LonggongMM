using System;
using System.Collections.Generic;
using Mavis.Core;
using NHibernate;
using NHibernate.Criterion;

namespace Mavis.DataAccess
{
    public class Repository<T>: RepositoryWithTypedId<T, int>, IRepository<T>
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">the type of entity</typeparam>
    /// <typeparam name="T1">the type of Id</typeparam>
    public class RepositoryWithTypedId<T, T1> : IRepositoryWithTypedId<T, T1>
    {
        private IDBContext _dbContext;

        protected virtual ISession Session
        {
            get
            {
                return NHibernateSession.Current;
            }
        }

        public virtual IDBContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new DBContext();
                }

                return _dbContext;
            }
        }

        #region Implementation of IRepositoryWithTypedId<T,T1>

        public virtual T Get(T1 id)
        {
            return Session.Get<T>(id);
        }

        public virtual IList<T> GetAll()
        {
            ICriteria criteria = Session.CreateCriteria(typeof (T));
            return criteria.List<T>();
        }

        public virtual IList<T> FindAll(IDictionary<string, object> propertyValuePairs)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));

            foreach (var key in propertyValuePairs.Keys)
            {
                if (propertyValuePairs[key] != null)
                {
                    criteria.Add(Restrictions.Eq(key, propertyValuePairs[key]));
                }
                else
                {
                    criteria.Add(Restrictions.IsNull(key));
                }
            }

            return criteria.List<T>();
        }

        public virtual IList<T> FindByCriterias(List<SearchCriteria<T>> searchCriterias)
        {
            var criteria = Session.CreateCriteria(typeof(T));

            foreach (var searchCriteria in searchCriterias)
            {
                if (searchCriteria.PropertyValue == null ||
                    string.IsNullOrEmpty(searchCriteria.PropertyValue.ToString().Trim())) continue;
                if (!string.IsNullOrEmpty(searchCriteria.AssociationPath))
                    criteria.CreateCriteria(searchCriteria.AssociationPath).Add(FromSearchCriteria(searchCriteria));
                else
                    criteria.Add(FromSearchCriteria(searchCriteria));
            }
            return criteria.List<T>();
        }


        public virtual T FindOne(IDictionary<string, object> propertyValuePairs)
        {
            var foundList = FindAll(propertyValuePairs);

            if (foundList.Count > 1)
            {
                throw new NonUniqueResultException(foundList.Count);
            }
            return foundList.Count == 1 ? foundList[0] : default(T);
        }

        public virtual T SaveOrUpdate(T entity)
        {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }

        #endregion

        protected AbstractCriterion FromSearchCriteria<T2>(SearchCriteria<T2> searchCriteria)
        {
            var propertyName = searchCriteria.PropertyExpression == null
                                       ? searchCriteria.PropertyName
                                       : ReflectionHelper.GetPropertyName(searchCriteria.PropertyExpression);
            var value = searchCriteria.PropertyValue;
            if (value is string)
                value = (value as string).Trim();

            switch (searchCriteria.Operator)
            {
                case Operator.Equal:
                    return Restrictions.Eq(propertyName, value);
                case Operator.Like:
                    return Restrictions.Like(propertyName, value.ToString(), MatchMode.Anywhere);
                case Operator.GreaterThan:
                    return Restrictions.Gt(propertyName, value);
                case Operator.LessThan:
                    return Restrictions.Lt(propertyName, value);
                case Operator.LessEqual:
                    return Restrictions.Le(propertyName, value);
                case Operator.GreaterEqual:
                    return Restrictions.Ge(propertyName, value);
                default:
                    return Restrictions.Eq(propertyName, value);
            }
        }
    }
}