using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class AreaRepository : BaseRepository<Area>
    {
        public AreaRepository(DbContext context)
        {
            Context = context;
        }


        public override void Add(Area area)
        {
            try
            {
                Context.Set<Area>().Add(area);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override bool Has(Area area)
        {
            try
            {
                return Context.Set<Area>().Contains(area);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override IEnumerable<Area> GetAll()
        {
            try
            {
                return Context.Set<Area>().ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override Area GetByName(string area)
        {
            try
            {
                return Context.Set<Area>().Where(x => x.Name == area).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override Area GetByID(int id)
        {
            try
            {
                return Context.Set<Area>().Include(x => x.AreaUsers)
                .Where(x => x.ID == id).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }


    }
}