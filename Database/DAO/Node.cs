using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.DAO
{
    public class Node
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(128)]
        public string Label { get; set; }
        public virtual ICollection<Node> AdjacentNodes { get; set; }
    }
}
