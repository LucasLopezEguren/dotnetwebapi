using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using CompanyDashboard.DataAccess.Interface;
using System.Linq.Expressions;
using System.Linq;

namespace CompanyDashboard.DataAccess
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; set; }
        
        public void Save()
        {
            Context.SaveChanges();
        }
        public abstract void Add(T entity);

        public void Delete(T entity) 
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().Where(expression);
        }
 
        public virtual T GetFirst(Expression<Func<T, bool>> expression)
        {
            return Context.Set<T>().First(expression);
        }

        public abstract bool Has(T entity);

        public abstract IEnumerable<T> GetAll();

        public abstract T GetByName(string name);

        public abstract T GetByID(int id);

        public void Update(T entity) 
        {
            Context.Attach(entity).State = EntityState.Modified;
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        


    }
}