using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DAO;

namespace Database
{
    public class GraphContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }
        public DbSet<AdjacentNode> AdjacentNodes { get; set; }
    }
}
