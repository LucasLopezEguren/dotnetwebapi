using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class AreaUserRepository : BaseRepository<AreaUser>
    {
        public AreaUserRepository(DbContext context)
        {
            Context = context;
        }

        public override void Add(AreaUser entity)

        {
            try
            {
                Area area = Context.Set<Area>().Where(x => x.ID == entity.AreaID).FirstOrDefault();
                Context.Set<Area>().Attach(area);
                User user = Context.Set<User>().Where(x => x.ID == entity.UserID).FirstOrDefault();
                Context.Set<User>().Attach(user);

                entity.user = user;
                entity.area = area;
                Context.Set<AreaUser>().Add(entity);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override AreaUser GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUsersByArea(Area area)
        {
            UserRepository userRepository = new UserRepository(ContextFactory.GetNewContext());
            try
            {
                List<AreaUser> areaUser = Context.Set<AreaUser>().Include(x => x.user)
                .Where(x => x.AreaID == area.ID).ToList();
                List<User> result = new List<User>();

                foreach (AreaUser au in areaUser)
                {
                    result.Add(userRepository.GetByID(au.UserID));
                }
                return result;
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public AreaUser GetAreaUser(Area area, User user)
        {
            try
            {
                return Context.Set<AreaUser>().Where(x => x.AreaID == area.ID &&
                x.UserID == user.ID).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public List<Area> GetAreasByUser(User user)
        {
            try
            {
                List<AreaUser> areaUser = Context.Set<AreaUser>().Include(x => x.area)
                .Where(x => x.UserID == user.ID).ToList();
                List<Area> result = new List<Area>();

                foreach (AreaUser au in areaUser)
                {
                    result.Add(au.area);
                }
                return result;
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override IEnumerable<AreaUser> GetAll()
        {
            try
            {
                return Context.Set<AreaUser>().ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override AreaUser GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public override bool Has(AreaUser entity)
        {
            try
            {
                return Context.Set<AreaUser>().Contains(entity);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }


    }
}