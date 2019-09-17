using System;
using System.Linq;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using CompanyDashboard.Domain;
using CompanyDashboard.DataAccess.Exceptions;

namespace CompanyDashboard.DataAccess
{
    public class IndicatorRepository : BaseRepository<Indicator>
    {
        public IndicatorRepository(DbContext context)
        {
            Context = context;
        }

        public override void Add(Indicator indicator)
        {
            try
            {
                Context.Set<Indicator>().Add(indicator);
                Context.SaveChanges();
                Node green = Context.Set<Node>().Where(x => x.ID == indicator.Green.ID).FirstOrDefault();
                Context.Set<Node>().Attach(green);
                Node yellow = Context.Set<Node>().Where(x => x.ID == indicator.Yellow.ID).FirstOrDefault();
                Context.Set<Node>().Attach(yellow);
                Node red = Context.Set<Node>().Where(x => x.ID == indicator.Red.ID).FirstOrDefault();
                Context.Set<Node>().Attach(red);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override bool Has(Indicator indicator)
        {
            try
            {
                return Context.Set<Indicator>().Contains(indicator);
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override Indicator GetByID(int id)
        {
            try
            {
                Indicator toReturn = Context.Set<Indicator>()
                                        .Include("Green.Left")
                                        .Include("Yellow.Left")
                                        .Include("Red.Left")
                                        .Include("Green.Right")
                                        .Include("Yellow.Right")
                                        .Include("Red.Right")
                                        .Where(x => x.ID == id).FirstOrDefault();
                toReturn.Green.Left = getNodes(toReturn.Green.Left);
                toReturn.Yellow.Left = getNodes(toReturn.Yellow.Left);
                toReturn.Red.Left = getNodes(toReturn.Red.Left);
                toReturn.Green.Right = getNodes(toReturn.Green.Right);
                toReturn.Yellow.Right = getNodes(toReturn.Yellow.Right);
                toReturn.Red.Right = getNodes(toReturn.Red.Right);

                return toReturn;
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }

        }

        public override IEnumerable<Indicator> GetAll()
        {
            try
            {
                List<Indicator> toReturn = new List<Indicator>();
                List<Indicator> indicators = Context.Set<Indicator>()
                                            .Include("Green.Left")
                                            .Include("Green.Right")
                                            .Include("Yellow.Left")
                                            .Include("Yellow.Right")
                                            .Include("Red.Left")
                                            .Include("Red.Right")
                                            .ToList();
                foreach(Indicator indicator in indicators){
                indicator.Green.Left = getNodes(indicator.Green.Left);
                indicator.Yellow.Left = getNodes(indicator.Yellow.Left);
                indicator.Red.Left = getNodes(indicator.Red.Left);
                indicator.Green.Right = getNodes(indicator.Green.Right);
                indicator.Yellow.Right = getNodes(indicator.Yellow.Right);
                indicator.Red.Right = getNodes(indicator.Red.Right);
                toReturn.Add(indicator);
                }
                return toReturn;
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        public override Indicator GetByName(string name)
        {
            try
            {
                return Context.Set<Indicator>().Include(x => x.Green).Include(x => x.Yellow).Include(x => x.Red).Where(x => x.Name == name).FirstOrDefault();
            }
            catch (DbException) { throw new DataBaseRepositoryException("Error en la conexión con la base de datos"); }
            catch (InvalidOperationException) { throw new InvalidOperationRepositoryException("Error en el sistema"); }
        }

        private Node getNodes(Node node)
        {
            NodeRepository nodeRepository = new NodeRepository();
            Node aNode = nodeRepository.GetByID(node.ID);
            if (aNode.Left == null || aNode.Right == null)
            {
                return aNode;
            }
            aNode.Left = getNodes(aNode.Left);
            aNode.Right = getNodes(aNode.Right);
            return aNode;
        }
    }
}