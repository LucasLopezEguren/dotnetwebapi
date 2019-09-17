using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class LogRepository : BaseRepository<Log>
    {
        public LogRepository(DbContext context)
        {
            Context = context;
        }
        public override void Add(Log log)
        {
            try
            {
                Context.Set<Log>().Add(log);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override bool Has(Log log)
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<Log> GetAll()
        {
            try
            {
                return Context.Set<Log>().ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override Log GetByName(string log)
        {
            try
            {
                return Context.Set<Log>().Where(x => x.Username == log).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override Log GetByID(int id)
        {
            throw new NotImplementedException();
        }
    }
}