using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebServices.Service
{
    [DataContract]
    public class NodeDTO
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public List<int> AdjacentNodes { get; set; }
    }
}