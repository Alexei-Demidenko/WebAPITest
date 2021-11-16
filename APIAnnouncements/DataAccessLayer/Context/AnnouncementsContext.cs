using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class AnnouncementsContext : DbContext
    {
        public AnnouncementsContext(DbContextOptions<AnnouncementsContext> options) : base(options)
        { }
        public DbSet<Announcing> Announcings { get; set; }
        public DbSet<User> Users { get; set; }
        public AnnouncementsContext(string connectionString)
       //     : base(connectionString)
        {
        }
    }
}
