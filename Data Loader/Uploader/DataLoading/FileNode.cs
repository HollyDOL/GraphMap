using System.Collections.Generic;
using System.Xml.Serialization;

namespace DataLoader.DataLoading
{
    [XmlRoot("node")]
    public class FileNode
    {
        [XmlArray("adjacentNodes"),XmlArrayItem("id")]
        public List<int> AdjacentNodes;

        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("label")]
        public string Label { get; set; }

        public override string ToString()
        {
            return $"{Id} - \"{Label}\" ({string.Join(", ", AdjacentNodes)})";
        }
    }
}