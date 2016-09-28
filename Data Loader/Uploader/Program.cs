using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataLoader.DataProcessing;

namespace DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                PrintHelp();
                return;
            }
            string rootFolder = args[0];

            if (!Directory.Exists(rootFolder))
            {
                Console.Error.WriteLine($"Specified Path \"{rootFolder}\"does not exist!");
                return;
            }

            IEnumerable<FileNode> allNodes = FileParser.LoadNodes(DirectoryCrawler.GetAllXmlsInFolder(rootFolder));
            foreach (FileNode fn in allNodes)
            {
                Console.Out.WriteLine(fn);
            }
            Console.ReadKey();
        }

        private static void PrintHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("\t\"Data Loader.exe\" \"<PathToDataSamplesFolder>\"");
        }
    }
}
