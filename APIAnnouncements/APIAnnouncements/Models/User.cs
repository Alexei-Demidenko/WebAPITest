using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAnnouncements.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public bool Admin { get; set; }
    }
}
