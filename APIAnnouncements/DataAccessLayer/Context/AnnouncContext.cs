using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class AnnouncContext : DbContext
    {
        public AnnouncContext(DbContextOptions<AnnouncContext> options) : base(options)
        { }
        public DbSet<Announcing> Announcings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
