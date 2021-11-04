using System;

namespace APIAnnouncements.Models
{
    public class Announcing
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
