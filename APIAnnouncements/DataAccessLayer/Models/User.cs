using System;

namespace DataAccessLayer.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool Admin { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
