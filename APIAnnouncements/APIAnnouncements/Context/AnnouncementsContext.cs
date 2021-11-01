using Microsoft.EntityFrameworkCore;
using APIAnnouncements.Models;

namespace APIAnnouncements.Context
{
    public class AnnouncementsContext : DbContext
    {
        public AnnouncementsContext(DbContextOptions<AnnouncementsContext> options) : base(options)
        { }
        public DbSet<Announcing> Announcings { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
