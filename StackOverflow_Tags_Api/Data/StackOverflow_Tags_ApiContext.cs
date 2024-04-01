using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StackOverflow_Tags_Api.Models;

namespace StackOverflow_Tags_Api.Data
{
    public class StackOverflow_Tags_ApiContext : DbContext
    {
        public StackOverflow_Tags_ApiContext(DbContextOptions<StackOverflow_Tags_ApiContext> options) : base(options)
        {
            if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator dbCreater)
            {
                // Create Database
                if (!dbCreater.CanConnect())
                {
                    dbCreater.Create();
                }

                // Create Tables
                if (!dbCreater.HasTables())
                {
                    dbCreater.CreateTables();
                }
            }
        }

        public DbSet<Tag> Tag { get; set; } = default!;
        public DbSet<Collective> Collective { get; set; } = default!;
        public DbSet<CollectiveExternalLink> CollectiveExternalLink { get; set; } = default!;
    }
}