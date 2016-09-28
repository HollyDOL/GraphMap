using System.Collections.Generic;
using System.IO;

namespace DataLoader.DataLoading
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