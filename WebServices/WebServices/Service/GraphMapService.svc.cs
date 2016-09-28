using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Database;
using Database.DAO;

namespace WebServices.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "GraphMapService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select GraphMapService.svc or GraphMapService.svc.cs at the Solution Explorer and start debugging.
    public class GraphMapService : IGraphMapService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public List<NodeDTO> GetAllData()
        {
            List<NodeDTO> result = new List<NodeDTO>();
            using (GraphContext context = new GraphContext())
            {
                foreach (Node node in context.Nodes)
                {
                    NodeDTO dto = new NodeDTO()
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
            throw new NotImplementedException();
        }
    }
}
