using TestingFramework;
using Microsoft.EntityFrameworkCore;

namespace TestingFramework.DbClient
{
    class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigManager.AppSettings["DbClient.ConnectionUrl"]);
        }
    }
}
