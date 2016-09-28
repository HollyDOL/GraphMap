using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NLog;

namespace DataLoader.DataLoading
{
    internal static class FileParser
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        internal static IEnumerable<FileNode> LoadNodes(IEnumerable<FileInfo> files, bool asParallel = true)
        {
            return asParallel ? LoadParalel(files) : LoadSerial(files);
        }

        private static IEnumerable<FileNode> LoadSerial(IEnumerable<FileInfo> files)
        {
            List<FileNode> result = new List<FileNode>();
            foreach (FileInfo info in files)
            {
                FileNode node = LoadFile(info);
                if (node != null) result.Add(node);
            }

            return result;
        }

        private static IEnumerable<FileNode> LoadParalel(IEnumerable<FileInfo> files)
        {
            ConcurrentBag<FileNode> result = new ConcurrentBag<FileNode>();
            Parallel.ForEach(files, info =>
            {
                FileNode node = LoadFile(info);
                if (node != null) result.Add(node);
            });
            return result;
        }

        private static FileNode LoadFile(FileInfo fi)
        {
            TextReader tr = null;
            try
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(FileNode));
                tr = new StreamReader(fi.FullName);
                return (FileNode) deserializer.Deserialize(tr);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Unable to process file {fi.Name}");
            }
            finally
            {
                tr?.Close();
            }

            return null;
        }
    }
}