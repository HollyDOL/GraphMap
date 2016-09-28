using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Database.DAO;

namespace Uploader
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
            using (GraphContext context = new GraphContext())
            {
                foreach (Node n in context.Nodes)
                {
                    Console.Out.WriteLine(n.Label);
                }
                Console.Out.WriteLine("a to je vsechno");
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
