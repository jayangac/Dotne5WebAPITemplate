using Dotne5WebAPITemplate.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dotne5WebAPITemplate.DAL.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IEntityBase, new()
    {
        protected DbContext DbContext;

        protected DbSet<T> Set { get; }

        protected IQueryable<T> Query { get; set; }

        public BaseRepository(DbContext context)
        {
            DbContext = context;
            Set = DbContext.Set<T>();
            Query = this.Set;
        }



        #region Non Async Functions
        public virtual IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public virtual IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbContext.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public virtual bool Any(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return Query.Any(filter);
            }
            else
            {
                return Query.Any();
            }
        }

        public virtual List<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = Query;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public virtual T GetById(int id, string includes = null)
        {
            IQueryable<T> query = Query;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }

            return query.FirstOrDefault(t => t.ID.Equals(id) && t.Status == true);
        }

        public virtual T GetByIdWithDisabled(int id, string includes = null)
        {
            IQueryable<T> query = Set;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }
            return query.FirstOrDefault(t => t.ID.Equals(id));
        }

        public T GetSingle(Expression<Func<T, bool>> filter, string includes = null)
        {
            IQueryable<T> query = Query;
            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }

            if (filter != null)
            {
                var rtv = query.SingleOrDefault(filter);
                return rtv;
            }
            else
            {
                var rtv = query.FirstOrDefault();
                return rtv;
            }
        }

        public virtual T Add(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Added;
            DbContext.Set<T>().Add(entity);
            return entity;
        }

        public virtual void Update(T entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                this.Set.Attach(entity);
            }

            EntityEntry dbEntityEntry = DbContext.Entry<T>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Delete(int id)
        {
            var entityToDelete = Query.FirstOrDefault(t => t.ID.Equals(id));
            EntityEntry dbEntityEntry = DbContext.Entry<T>(entityToDelete);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public List<T> AddRange(List<T> entityList)
        {
            Set.AddRange(entityList);
            return entityList;
        }

        public virtual void UpdateRange(List<T> entityListToUpdate)
        {
            DbContext.Entry(entityListToUpdate).State = EntityState.Modified;
        }
        #endregion


        #region Async Function

        public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
            {
                return Query.AnyAsync(filter);
            }
            else
            {
                return Query.AnyAsync();
            }
        }

        public virtual Task<List<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includes = null,
            int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = Query;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToListAsync();
        }

        public virtual Task<T> GetByIdAsync(int id, string includes = null)
        {
            IQueryable<T> query = Query;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }

            return query.FirstOrDefaultAsync(t => t.ID.Equals(id) && t.Status == true);
        }

        public virtual Task<T> GetByIdWithDisabledAsync(int id, string includes = null)
        {
            IQueryable<T> query = Set;

            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }
            return query.FirstOrDefaultAsync(t => t.ID.Equals(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter, string includes = null)
        {
            IQueryable<T> query = Query;
            if (!string.IsNullOrWhiteSpace(includes))
            {
                query = IncludeProperties(query, includes);
            }

            if (filter != null) // TODO - more than one result is in filter. So exception throws.
            {
                var rtv2 = await query.SingleOrDefaultAsync(filter);
                return rtv2;
            }
            else
            {
                var rtv = await query.FirstOrDefaultAsync(filter);
                return rtv;
            }
        }

        #endregion

        private static IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties)
        {
            foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return query;
        }
    }
}
