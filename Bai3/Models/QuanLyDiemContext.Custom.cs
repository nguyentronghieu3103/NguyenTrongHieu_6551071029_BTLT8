using Microsoft.EntityFrameworkCore;
using System;

namespace Bai3.Models
{
    public partial class QuanLyDiemContext
    {
        private static string? _connectionString;

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    _connectionString = DetermineConnectionString();
                }
                return _connectionString;
            }
            set => _connectionString = value;
        }

        private static string DetermineConnectionString()
        {
            string[] candidates = new[]
            {
                "Server=.\\SQLEXPRESS;Database=QuanLyDiem;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;",
                "Server=.;Database=QuanLyDiem;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;",
                "Server=(localdb)\\mssqllocaldb;Database=QuanLyDiem;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=2;"
            };

            foreach (var connStr in candidates)
            {
                try
                {
                    var options = new DbContextOptionsBuilder<QuanLyDiemContext>()
                        .UseSqlServer(connStr)
                        .Options;
                    using var context = new QuanLyDiemContext(options);
                    if (context.Database.CanConnect())
                    {
                        return connStr;
                    }
                }
                catch
                {
                    // Ignore and try next
                }
            }

            return candidates[0];
        }

        public QuanLyDiemContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
