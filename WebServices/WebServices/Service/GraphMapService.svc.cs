using System.Collections.Generic;
using System.Linq;
using Database;
using Database.DAO;
using WebServices.PathFinding;

namespace WebServices.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GraphMapService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GraphMapService.svc or GraphMapService.svc.cs at the Solution Explorer and start debugging.
    public class GraphMapService : IGraphMapService
    {
        public List<NodeDTO> GetAllData()
        {
            List<NodeDTO> result = new List<NodeDTO>();
            using (GraphContext context = new GraphContext())
            {
                foreach (Node node in context.Nodes)
                {
                    NodeDTO dto = new NodeDTO
                    {
                        Id = node.Id,
                        Label = node.Label,
                        AdjacentNodes = new List<int>(node.AdjacentNodes.Select(adj => adj.Id))
                    };
                    result.Add(dto);
                }
            }
            return result;
        }

        public List<int> GetWayBetween(int source, int destination)
        {
            IPathFinder pathFinder = new BreadthFirstSearch();
            List<int> result = pathFinder.GetPathBetween(source, destination);
            return result;
        }
    }
}