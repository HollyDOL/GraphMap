using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Database;
using Database.DAO;

namespace WebServices.PathFinding
{
    public abstract class AbstractPathFinder : IPathFinder
    {
        public abstract List<int> GetPathBetween(int sourceNode, int destinationNode);

        protected bool TryFindNode(int id, out Node node)
        {
            using (GraphContext context = new GraphContext())
            {
                node = context.Nodes.FirstOrDefault(n => n.Id == id);
                return (node != null);
            }
        }
    }
}