using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAnnouncements.Models
{
    public class Announcing
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
