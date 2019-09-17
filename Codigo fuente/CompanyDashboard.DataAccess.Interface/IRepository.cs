using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompanyDashboard.DataAccess.Interface
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Add(T entity);

        void Save();
        void Delete(T entity);
        void Update(T entity);
        bool Has(T entity);
        IEnumerable<T> GetAll();
        T GetByName(string name);
        T GetByID(int id);
        IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression);
        T GetFirst(Expression<Func<T, bool>> expression);
        
    }
    
}
