using System.Collections.Generic;

namespace WebServices.PathFinding
{
    public interface IPathFinder
    {
        List<int> GetPathBetween(int sourceNode, int destinationNode);
    }
}