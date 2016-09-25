using MEZBELE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

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