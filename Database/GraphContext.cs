using System.Data.Entity;
using Database.DAO;

namespace Database
{
    public class GraphContext : DbContext
    {
        public DbSet<Node> Nodes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>().HasMany(c => c.AdjacentNodes).WithMany().Map(m =>
            {
                m.MapLeftKey("originID");
                m.MapRightKey("destinationID");
                m.ToTable("Node_to_Node");
            });
        }
    }
}