using System.Collections.Generic;
using System.IO;

namespace DataLoader.DataProcessing
{
    internal static class DirectoryCrawler
    {
        internal static IList<FileInfo> GetAllXmlsInFolder(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            return di.GetFiles("*.xml", SearchOption.AllDirectories);
        }
    }
}