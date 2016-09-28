using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Database;
using Database.DAO;
using DataLoader.DataLoading;
using DataLoader.DataSynchronization;
using NLog;

namespace DataLoader
{
    class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            Logger.Info("Data Loader starting");
            if (args.Length != 1)
            {
                PrintHelp();
                return;
            }
            string rootFolder = args[0];

            if (!Directory.Exists(rootFolder))
            {
                Logger.Error($"Specified Path \"{rootFolder}\"does not exist!");
                return;
            }

            UpdateDatabase(rootFolder);
            Logger.Info("End of Program");
        }

        private static void UpdateDatabase(string rootFolder)
        {
            LogSnapshot("Database state before update");

            IEnumerable<FileNode> allNodes = FileParser.LoadNodes(DirectoryCrawler.GetAllXmlsInFolder(rootFolder));
            Synchronizer syncer = new Synchronizer(allNodes);
            syncer.Synchronize();
            syncer.Dispose();

            LogSnapshot("Database state after update");
        }

        private static void LogSnapshot(string header)
        {
            Logger.Info(header);
            using (GraphContext context = new GraphContext())
            {
                foreach (Node node in context.Nodes)
                {
                    Logger.Info(node);
                }
            }
        }

        private static void PrintHelp()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Usage:");
            sb.AppendLine("\t\"Data Loader.exe\" \"<PathToDataSamplesFolder>\"");
        }
    }
}