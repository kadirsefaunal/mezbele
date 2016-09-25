using MEZBELE.Models;
using System.Data.Entity;

namespace MEZBELE.Context
{
    public class MezbeleContext : DbContext
    {
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Issues> Issues { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Crews> Crews { get; set; }
    }
}