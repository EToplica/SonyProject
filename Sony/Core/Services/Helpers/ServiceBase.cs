using Sony.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sony.Core.Services.Helpers
{
    public abstract class ServiceBase<TEntity> : ContextBase<TEntity>, IServiceBase<TEntity> where TEntity : class, new()
    {
        public ServiceBase(ApplicationDbContext context) : base(context) { }

        public TEntity Add(TEntity entity)
        {
            __THIS__.Add(entity);
            Save();
            return entity;
        }


        public async Task AddAsync(TEntity entity)
        {
            await __THIS__.AddAsync(entity);
            await SaveAsync();
        }


        public void AddRange(IEnumerable<TEntity> entities)
        {
            __THIS__.AddRange(entities);
            Save();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await __THIS__.AddRangeAsync(entities);
            await SaveAsync();
        }

        public TEntity Get(int id) => __THIS__.Find(id);


        public IEnumerable<TEntity> GetAll() => __THIS__.ToList();


        public async Task<TEntity> GetAsync(int id) => await __THIS__.FindAsync(id);


        public IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate) => __THIS__.Where(predicate);


        public void Remove(TEntity entity)
        {
            __THIS__.Remove(entity);
            Save();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            __THIS__.RemoveRange(entities);

            Save();
        }

        public void Update(TEntity entity)
        {
            __THIS__.Update(entity);
            Save();
        }

        public TEntity UpdateAndGet(TEntity entity)
        {
            var ret = __THIS__.Update(entity);
            Save();
            return ret.Entity;

        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            __THIS__.UpdateRange(entities);
            Save();
        }
    }
}
