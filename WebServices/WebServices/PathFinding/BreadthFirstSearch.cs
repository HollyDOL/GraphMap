using System.Collections.Generic;
using System.Linq;
using Database;
using Database.DAO;

namespace WebServices.PathFinding
{
    public class BreadthFirstSearch : AbstractPathFinder
    {
        private readonly HashSet<Node> _alreadyVisited;
        private bool _searched;
        private List<int> _way;

        public BreadthFirstSearch()
        {
            _alreadyVisited = new HashSet<Node>();
            _way = null;
        }

        private IEnumerable<WorkNode> ExpandNode(WorkNode root)
        {
            HashSet<WorkNode> result = new HashSet<WorkNode>();
            foreach (Node node in root.Node.AdjacentNodes)
            {
                if (_alreadyVisited.Contains(node)) continue;
                WorkNode wn = new WorkNode(root, node);
                result.Add(wn);
            }

            using (GraphContext context = new GraphContext())
            {
                foreach (Node n in context.Nodes.Where(n => n.AdjacentNodes.Contains(root.Node)))
                {
                    if (_alreadyVisited.Contains(n)) continue;
                    WorkNode wn = new WorkNode(root, n);
                    result.Add(wn);
                }
            }
            return result;
        }

        public override List<int> GetPathBetween(int sourceNode, int destinationNode)
        {
            if (_searched) return _way;
            _searched = true;
            Queue<WorkNode> visits = new Queue<WorkNode>();

            Node source;
            Node destination;
            if (!TryFindNode(sourceNode, out source) || !TryFindNode(destinationNode, out destination))
                return null;

            visits.Enqueue(new WorkNode(null, source));
            bool found = false;
            WorkNode current = null;
            while (visits.Any())
            {
                current = visits.Dequeue();
                _alreadyVisited.Add(current.Node);
                if (current.Node == destination)
                {
                    found = true;
                    break;
                }
                if (_alreadyVisited.Contains(current.Node)) continue;

                foreach (WorkNode wrk in ExpandNode(current))
                    visits.Enqueue(wrk);
            }

            if (!found) return null;

            Stack<int> st = new Stack<int>();
            while (current != null)
            {
                st.Push(current.Node.Id);
                current = current.ParentNode;
            }

            _way = new List<int>(st);
            return _way;
        }
    }
}