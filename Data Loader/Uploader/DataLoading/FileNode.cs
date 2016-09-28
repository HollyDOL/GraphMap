using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataLoader.DataProcessing
{
    [XmlRoot("node")]
    public class FileNode
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("label")]
        public string Label { get; set; }

        [XmlArray("adjacentNodes")]
        [XmlArrayItem("id")]
        public List<int> AdjacentNodes;

        public override string ToString()
        {
            return $"{Id} - \"{Label}\" ({string.Join(", ", AdjacentNodes)})";
        }
    }
}
