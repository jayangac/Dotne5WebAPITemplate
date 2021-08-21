using Dotne5WebAPITemplate.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dotne5WebAPITemplate.DAL.Base
{
    public interface IBaseRepository<T> where T : class, IEntityBase, new()
    {

        #region Non Async funtions

        /// <summary>
        /// Check is there any record in the database async, using the filter options given
        /// </summary>
        /// <param name="filter">Expression to search</param>
        /// <returns>A boolean. if there is any record return true if not false</returns>
        bool Any(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Get all as Iqueryable
        /// </summary>
        /// <returns>IQueryable of the entity</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get all details with include entities
        /// </summary>
        /// <param name="includeProperties">Include entities using properties</param>
        /// <returns>Return entity as a quaryable</returns>
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Basic get by id where status true
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <param name="includes">includes of dependencies, {comma seperated string}</param>
        /// <returns>return entity</returns>
        T GetById(int id, string includes = null);

        /// <summary>
        /// Basic get
        /// </summary>
        /// <param name="filter">lambda expresion to filter</param>
        /// <param name="orderBy">lamba expresion to order</param>
        /// <param name="includes">includes of dependencies, {comma seperated string}</param>
        /// <param name="page">actual page</param>
        /// <param name="pageSize">number of results by page</param>
        /// <returns>list of entities</returns>
        List<T> Get(
            Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includes = null,
           int? page = null,
           int? pageSize = null);

        /// <summary>
        /// Get by id where status not consider
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <param name="includes">includes of dependencies, {comma seperated string}</param>
        /// <returns>return entity</returns>
        T GetByIdWithDisabled(int id, string includes = null);

        /// <summary>
        /// Basic get by filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="includes">includes</param>
        /// <returns>return entity</returns>
        T GetSingle(Expression<Func<T, bool>> filter, string includes = null);

        void UpdateRange(List<T> entityListToUpdate);

        List<T> AddRange(List<T> entityList);

        T Add(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Update(T entity);

        #endregion


        #region Async functions


        /// <summary>
        /// Check is there any record in the database async, using the filter options given
        /// </summary>
        /// <param name="filter">Expression to search</param>
        /// <returns>Task with the result. if there is any record return true if not false</returns>
        /// 
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Basic get by id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <param name="includes">includes of dependencies</param>
        /// <returns>return entity encapsulated in maybe functionality</returns>
        Task<T> GetByIdAsync(int id, string includes = null);

        /// <summary>
        /// Basic get
        /// </summary>
        /// <param name="filter">lambda expresion to filter</param>
        /// <param name="orderBy">lamba expresion to order</param>
        /// <param name="includes">includes of dependencies, {comma seperated string}</param>
        /// <param name="page">actual page</param>
        /// <param name="pageSize">number of results by page</param>
        /// <returns>list of entities</returns>
        Task<List<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includes = null,
           int? page = null,
           int? pageSize = null);

        /// <summary>
        /// Get by id where status not consider
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <param name="includes">includes of dependencies, {comma seperated string}</param>
        /// <returns>return entity encapsulated in maybe functionality</returns>
        Task<T> GetByIdWithDisabledAsync(int id, string includes = null);

        /// <summary>
        /// Basic get by filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <param name="includes">includes</param>
        /// <returns>return entity encapsulated in maybe functionality</returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, string includes = null);

        #endregion




    }
}
