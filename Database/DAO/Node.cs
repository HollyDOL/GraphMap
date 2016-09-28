using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Database.DAO
{
    public class Node
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Label { get; set; }
        public virtual ICollection<Node> AdjacentNodes { get; set; }

        public override string ToString()
        {
            return $"{Id} - \"{Label}\" ({string.Join(", ", AdjacentNodes.Select(adj => adj.Id))})";
        }

        public Node() { }
    }
}
