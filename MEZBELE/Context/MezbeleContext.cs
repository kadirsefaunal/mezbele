using MEZBELE.Models;
using System.Data.Entity;

namespace MEZBELE.Context
{
    /// <summary>
    /// Veritabanı kapsamı.
    /// </summary>
    public class MezbeleContext : DbContext
    {
        /// <summary>
        /// Yorumlar.
        /// </summary>
        public DbSet<Comments> Comments { get; set; }
        /// <summary>
        /// İşler.
        /// </summary>
        public DbSet<Issues> Issues { get; set; }
        /// <summary>
        /// Projeler.
        /// </summary>
        public DbSet<Projects> Projects { get; set; }
        /// <summary>
        /// Kullanıcılar.
        /// </summary>
        public DbSet<Users> Users { get; set; }
        /// <summary>
        /// Ekipler.
        /// </summary>
        public DbSet<Crews> Crews { get; set; }
    }
}