using System;
using Database.DAO;

namespace WebServices.PathFinding
{
    public class WorkNode : IEquatable<WorkNode>
    {
        public WorkNode(WorkNode parent, Node current)
        {
            ParentNode = parent;
            Node = current;
        }

        public WorkNode ParentNode { get; }
        public Node Node { get; }

        public bool Equals(WorkNode other)
        {
            if (other == null) return false;
            return ParentNode.Equals(other.ParentNode) && Node.Equals(other.Node);
        }
    }
}