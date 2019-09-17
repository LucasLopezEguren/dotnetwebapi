using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class UserIndicatorRepository : BaseRepository<UserIndicator>
    {
        public UserIndicatorRepository()
        {
            Context = ContextFactory.GetNewContext();
        }
        public override void Add(UserIndicator userIndicator)
        {
            try
            {
                Context.Set<UserIndicator>().Add(userIndicator);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override bool Has(UserIndicator userIndicator)
        {
            try
            {
                return Context.Set<UserIndicator>().Contains(userIndicator);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override UserIndicator GetByID(int id)
        {
            try
            {
                return Context.Set<UserIndicator>().Where(x => x.ID == id).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }

        }
        public override IEnumerable<UserIndicator> GetAll()
        {
            try
            {
                return Context.Set<UserIndicator>().ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public override UserIndicator GetByName(string username)
        {
            throw new NotImplementedException();
        }
        public UserIndicator Get(User user, Indicator indicator)
        {
            try
            {
                return Context.Set<UserIndicator>().Where(x => x.user == user.ID && x.indicator == indicator.ID).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
        public List<UserIndicator> GetIndicatorsByUser(User user)
        {
            try
            {
                return Context.Set<UserIndicator>().Where(x => x.user == user.ID).OrderBy(x => x.order).ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }
    }
}