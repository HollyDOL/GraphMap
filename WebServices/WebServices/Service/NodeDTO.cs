using System.Collections.Generic;
using System.Runtime.Serialization;

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