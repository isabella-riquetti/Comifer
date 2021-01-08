using Comifer.Data.Context;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Comifer.Data.Repository.Base
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        internal ComiferContext Context;
        internal DbSet<TEntity> DbSet;

        public RepositoryBase(ComiferContext context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
            this.Context.Configuration.LazyLoadingEnabled = true;
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void AddAll(List<TEntity> entity)
        {
            DbSet.AddRange(entity);
        }

        public void Edit(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void EditAll(List<TEntity> entity)
        {
            foreach (var item in entity)
            {
                Context.Entry<TEntity>(item).State = EntityState.Modified;
            }
        }

        public void Delete(TEntity entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }

        public void DeleteAll(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = DbSet;
            var listDelete = query.Where(filter).ToList();

            foreach (var item in listDelete)
            {
                DbSet.Remove(item);
            }
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            return orderBy?.Invoke(query)?.FirstOrDefault() ?? query?.FirstOrDefault();
        }

        public virtual TEntity SingleOrDefault(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            return orderBy?.Invoke(query)?.SingleOrDefault();
        }

        public void Dispose()
        {
            DbSet = null;
            Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
