using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

namespace ChatServer.Models
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> User { get; set; }

        public DbSet<ChatLog> ChatLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).ToList().ForEach(x =>
              {
                  x.DeleteBehavior = DeleteBehavior.Restrict;
              });
            base.OnModelCreating(modelBuilder);
        }
    }
}
