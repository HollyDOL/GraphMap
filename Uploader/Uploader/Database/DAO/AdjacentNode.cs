using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.DAO;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database
{
    public class AdjacentNode
    {
        [ForeignKey("Node")]
        public virtual Node Origin { get; set; }
        [ForeignKey("Node")]
        public virtual Node Destination { get; set; }
    }
}
