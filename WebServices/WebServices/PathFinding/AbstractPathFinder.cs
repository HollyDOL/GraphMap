using System.Collections.Generic;
using System.Linq;
using Database;
using Database.DAO;

namespace WebServices.PathFinding
{
    public abstract class AbstractPathFinder : IPathFinder
    {
        protected GraphContext Context = new GraphContext();
        public abstract List<int> GetPathBetween(int sourceNode, int destinationNode);

        protected bool TryFindNode(int id, out Node node)
        {
            node = Context.Nodes.FirstOrDefault(n => n.Id == id);
            return node != null;
        }
    }
}