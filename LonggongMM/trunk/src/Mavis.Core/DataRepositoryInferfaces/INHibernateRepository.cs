using System.Collections.Generic;

namespace Mavis.Core
{
    /// <summary>
    /// Extends the basic data repository interface with an interface that supports
    /// a number of NHibernate specific methods while avoiding a concreate dependency on the NHibernate assembly.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface INHibernateRepository<T>: INHibernateRepositoryWithTypedId<T, int>, IRepository<T>
    {}

    public interface INHibernateRepositoryWithTypedId<T, TId>
    {
        /// <summary>
        /// returns null if a row is not found matching the provided Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lockMode"></param>
        /// <returns></returns>
        T Get(TId id, CustomEnums.LockMode lockMode);

        /// <summary>
        /// throws an exception if a row is not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Load(TId id);

        /// <summary>
        /// throws an exception if a row is not found;
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lockMode"></param>
        /// <returns></returns>
        T Load(TId id, CustomEnums.LockMode lockMode);

        /// <summary>
        /// Looks for zero or more instances using the example provided.
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        IList<T> FindAll(T exampleInstance, params string[] propertiesToExclude);

        /// <summary>
        /// Looks for one instance using the example provided.
        /// </summary>
        /// <param name="exampleInstance"></param>
        /// <param name="propertiesToExclude"></param>
        /// <returns></returns>
        T FindOne(T exampleInstance, params string[] propertiesToExclude);

        /// <summary>
        /// For entities that have assigned Id's you must explicitly call Save to add a new one.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Save(T entity);

        /// <summary>
        /// For entities that have assigned Id's you must explicitly call Update to update an existing one
        /// Updating also allows you to commit changes to a detached object.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T Update(T entity);

        /// <summary>
        /// Dissasociates the entities with the ORM so that changes made to it are not automatically saved to
        /// the database. More precisely, this removes the entity from Session's cache.
        /// </summary>
        /// <param name="entity"></param>
        void Evict(T entity);
    }
}