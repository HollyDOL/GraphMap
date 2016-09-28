using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Database;
using Database.DAO;
using DataLoader.DataLoading;
using NLog;

namespace DataLoader.DataSynchronization
{
    internal class Synchronizer : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly GraphContext _context;
        private readonly IEnumerable<FileNode> _fileNodes;

        private bool _disposed;

        public Synchronizer(IEnumerable<FileNode> fileNodes)
        {
            _fileNodes = fileNodes;
            _context = new GraphContext();
        }

        public void Dispose()
        {
            Dispose(true);
        }


        private void DeleteOldNodes()
        {
            _context.Database.ExecuteSqlCommand("delete from dbo.Node_to_Node where 1=1");
            _context.Database.ExecuteSqlCommand("delete from dbo.Nodes where 1=1");
            _context.SaveChanges();
        }

        private HashSet<int> AddNodes()
        {
            HashSet<int> result = new HashSet<int>();
            foreach (FileNode fileNode in _fileNodes)
            {
                Node node = new Node
                {
                    Id = fileNode.Id,
                    Label = fileNode.Label
                };
                _context.Nodes.Add(node);
                result.Add(fileNode.Id);
            }
            _context.SaveChanges();
            return result;
        }

        private void AddReferences(Node node, HashSet<int> nodeIds)
        {
            _context.SaveChanges();
            int id = node.Id;
            FileNode fi = _fileNodes.First(fn => fn.Id == id);

            foreach (int destinationId in fi.AdjacentNodes)
            {
                if (nodeIds.Contains(destinationId))
                    _context.Database.ExecuteSqlCommand(
                        "insert into dbo.Node_to_Node Values (@originId, @destinationId)",
                        new SqlParameter("originId", id), new SqlParameter("destinationId", destinationId));
                else
                {
                    Logger.Warn($"Node {id} contains reference to node {destinationId} which does not exist. This reference is ignored.");
                }
            }

            _context.SaveChanges();
        }

        public void Synchronize()
        {
            DeleteOldNodes();
            HashSet<int> nodeIds = AddNodes();

            foreach (Node node in _context.Nodes.Local)
                AddReferences(node, nodeIds);
        }

        ~Synchronizer()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
    }
}