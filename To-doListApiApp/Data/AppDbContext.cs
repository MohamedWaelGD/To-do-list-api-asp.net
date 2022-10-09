using Microsoft.EntityFrameworkCore;
using To_doListApiApp.Models;

namespace To_doListApiApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserWorkspace>().HasKey(e => new { e.UserId, e.WorkspaceId});
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserWorkspace> UserWorkspaces { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
    }
}
