
using Dotne5WebAPITemplate.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dotne5WebAPITemplate.Services.Base
{
    public interface IBaseService<T> where T : IEntityBase
    {
        #region Gets

        /// <summary>
        /// Any function
        /// </summary>
        /// <param name="filter">filter of any</param>
        /// <returns>entities</returns>
        bool Any(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Any function
        /// </summary>
        /// <param name="filter">filter of any</param>
        /// <returns>entities</returns>
        Task<bool> AnySync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// Basic get
        /// </summary>
        /// <returns>list of entities</returns>
        List<T> Get();

        /// <summary>
        /// Basic Async get
        /// </summary>
        /// <returns>list of entities</returns>
        Task<List<T>> GetAsync();

        /// <summary>
        /// Basic get by id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>entity</returns>
        T Get(int id, string includes = null);

        /// <summary>
        /// Basic get by id async
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>entity</returns>
        Task<T> GetAsync(int id, string includes = null);

        /// <summary>
        /// get entity by id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>entities</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// get entity by id
        /// </summary>
        /// <param name="id">id of entity</param>
        /// <returns>entities</returns>
        T Get(int id);

        /// <summary>
        /// basic get by list of ids
        /// </summary>
        /// <param name="ids">ids of entities</param>
        /// <returns>list of entities</returns>
        List<T> Get(IEnumerable<int> ids);

        /// <summary>
        /// basic get by list of ids
        /// </summary>
        /// <param name="ids">ids of entities</param>
        /// <returns>list of entities</returns>
        Task<List<T>> GetAsync(IEnumerable<int> ids);
        #endregion

        #region Commands

        /// <summary>
        /// Basic create
        /// </summary>
        /// <param name="objectToCreate">entity to create</param>
        /// <returns>entity added</returns>
        T Create(T objectToCreate);

        /// <summary>
        /// Basic create
        /// </summary>
        /// <param name="objectsToCreate">list of entities to create</param>
        void Create(IEnumerable<T> objectsToCreate);

        /// <summary>
        /// Basic update with id and entity
        /// </summary>
        /// <param name="id">id of entity to update</param>
        /// <param name="objectToUpdate">entity to update</param>
        void Update(int id, T objectToUpdate);

        /// <summary>
        /// Basic update with entities
        /// </summary>
        /// <param name="objectsToUpdate">object to update</param>
        void Update(IEnumerable<T> objectsToUpdate);

        /// <summary>
        /// Basic remove by entity
        /// *******
        /// Delete never use, always try to use update to update the status to delete state
        /// *******
        /// </summary>
        /// <param name="objectToRemove">entity to remove</param>
        void Remove(T objectToRemove);

        /// <summary>
        /// Basic remove by id
        /// *******
        /// Delete never use, always try to use update to update the status to delete state
        /// *******
        /// </summary>
        /// <param name="id">id of entity to remove</param>
        /// <returns>return task</returns>
        void Remove(int id);

        /// <summary>
        /// basic remove by ids
        /// *******
        /// Delete never use, always try to use update to update the status to delete state
        /// *******
        /// </summary>
        /// <param name="ids">ids of entities to remove</param>
        /// <returns>return task</returns>
        void Remove(IEnumerable<int> ids);

        #endregion

        #region Others
        /// <summary>
        /// Save changes in context
        /// </summary>
        /// <returns>bool</returns>
        bool SaveChanges();

        /// <summary>
        /// Save changes async
        /// </summary>
        /// <returns>task with bool</returns>
        Task<bool> SaveChangesAsync();

        #endregion
    }
}
