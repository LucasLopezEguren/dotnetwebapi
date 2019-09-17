using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(DbContext context)
        {
            Context = context;
        }
        public override void Add(User user)
        {
            try
            {
                Context.Set<User>().Add(user);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override bool Has(User user)
        {
            try
            {
                return Context.Set<User>().Contains(user);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override User GetByID(int id)
        {
            try
            {
                return Context.Set<User>().Where(x => x.ID == id).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override IEnumerable<User> GetAll()
        {
            try
            {
                return Context.Set<User>().ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override User GetByName(string username)
        {
            try
            {
                return Context.Set<User>().Where(x => x.Username == username).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

    }
}