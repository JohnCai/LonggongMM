using System.Collections.Generic;

namespace Mavis.Core
{
    /// <summary>
    /// standard interface for DAOs
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    
    public interface IRepository<T>: IRepositoryWithTypedId<T, int>
    {
        
    }

    public interface IRepositoryWithTypedId<T, TId>
    {
        /// <summary>
        /// return null if not found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(TId id);

        /// <summary>
        /// return all of the items
        /// </summary>
        /// <returns></returns>
        IList<T> GetAll();

        /// <summary>
        /// Looks for instances using the filter condition provided.
        /// </summary>
        /// <param name="propertyValuePairs"></param>
        /// <returns></returns>
        IList<T> FindAll(IDictionary<string, object> propertyValuePairs);

        /// <summary>
        /// Looks for instances using searchCreiteria.
        /// </summary>
        /// <param name="searchCriterias"></param>
        /// <returns></returns>
        IList<T> FindByCriterias(List<SearchCriteria<T>> searchCriterias);

        /// <summary>
        /// Looks for one instance using the filter provided.
        /// </summary>
        /// <param name="propertyValuePairs"></param>
        /// <returns></returns>
        T FindOne(IDictionary<string, object> propertyValuePairs);

        /// <summary>
        /// For entities with automatatically generated Ids, this method could be called to save or update an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        T SaveOrUpdate(T entity);

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        IDBContext DbContext { get; }

    }
}