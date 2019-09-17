using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class NodeRepository : BaseRepository<Node>
    {
        public NodeRepository()
        {
            Context = ContextFactory.GetNewContext();
        }

        public override void Add(Node node)
        {
            try
            {
                Context.Set<Node>().Add(node); 
                Context.SaveChanges();
                Node left = Context.Set<Node>().Where(x => x.ID == node.Left.ID).FirstOrDefault();
                Context.Set<Node>().Attach(left);
                Node right = Context.Set<Node>().Where(x => x.ID == node.Right.ID).FirstOrDefault();
                Context.Set<Node>().Attach(right);

            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override bool Has(Node node)
        {
            try
            {
                return Context.Set<Node>().Contains(node);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override Node GetByID(int id)
        {
            try
            {
                return Context.Set<Node>().Include(x=> x.Right).Include(x=> x.Left).Where(x => x.ID == id).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }

        }

        public override IEnumerable<Node> GetAll()
        {
            try
            {
                return Context.Set<Node>().Include(x=> x.Right).Include(x=> x.Left).ToList();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override Node GetByName(string name)
        {
            try
            {
                return Context.Set<Node>().Include(x=> x.Right).Include(x=> x.Left).Where(x => x.ID.ToString() == name).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

    }
}