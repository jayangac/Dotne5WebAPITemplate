using Dotne5WebAPITemplate.DAL.Base;
using Dotne5WebAPITemplate.Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dotne5WebAPITemplate.Services.Base
{
    public class BaseService<T> : IBaseService<T> where T : class, IEntityBase, new()
    {
        private readonly IBaseRepository<T> _repository;
        protected BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public bool Any(Expression<Func<T, bool>> filter = null)
        {
            return _repository.Any(filter);
        }

        public Task<bool> AnySync(Expression<Func<T, bool>> filter = null)
        {
            return _repository.AnyAsync(filter);
        }

        public T Create(T objectToCreate)
        {
            return _repository.Add(objectToCreate);
        }

        public void Create(IEnumerable<T> objectsToCreate)
        {
            foreach (var entity in objectsToCreate)
            {
                Create(entity);
            }
        }

        public List<T> Get()
        {
            return _repository.Get();
        }

        public Task<List<T>> GetAsync()
        {
            return _repository.GetAsync();
        }
        public T Get(int id, string includes = null)
        {
            return _repository.GetById(id, includes);
        }

        public Task<T> GetAsync(int id, string includes = null)
        {
            return _repository.GetByIdAsync(id, includes);
        }

        public Task<List<T>> GetAsync(IEnumerable<int> ids)
        {
            return _repository.GetAsync(x => ids.Contains(x.ID));
        }
        public List<T> Get(IEnumerable<int> ids)
        {
            return _repository.Get(x => ids.Contains(x.ID));
        }

        public Task<T> GetAsync(int id)
        {
            return _repository.GetSingleAsync(x => x.ID.Equals(id));
        }
        public T Get(int id)
        {
            return _repository.GetSingle(x => x.ID.Equals(id));
        }

        public void Remove(T objectToRemove)
        {
            this._repository.Delete(objectToRemove);
        }

        public void Remove(int id)
        {
            this._repository.Delete(id);
        }

        public void Remove(IEnumerable<int> ids)
        {
            foreach (var id in ids)
            {
                this._repository.Delete(id);
            }
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(int id, T objectToUpdate)
        {
            objectToUpdate.ID = id;
            this._repository.Update(objectToUpdate);
        }

        public void Update(IEnumerable<T> objectsToUpdate)
        {
            foreach (var entity in objectsToUpdate)
            {
                this.Update(entity.ID, entity);
            }
        }
    }
}
