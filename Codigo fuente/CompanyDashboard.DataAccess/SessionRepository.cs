using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class SessionRepository : BaseRepository<Session>
    {
        public SessionRepository()
        {
            Context = ContextFactory.GetNewContext();
        }
        public override void Add(Session session)
        {
            try
            {
                Context.Set<Session>().Add(session);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override bool Has(Session session)
        {
            try
            {
                return Context.Set<Session>().Contains(session);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override Session GetByID(int id)
        {
            try
            {
                return Context.Set<Session>().Where(x => x.ID == id).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }

        }
        public override IEnumerable<Session> GetAll()
        {
            try
            {
                return Context.Set<Session>().ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override Session GetByName(string username)
        {
            try
            {
                return Context.Set<Session>().Where(x => x.username == username).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
    }
}