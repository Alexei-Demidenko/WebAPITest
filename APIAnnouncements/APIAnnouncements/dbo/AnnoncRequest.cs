using System;
using APIAnnouncements.Models;

namespace APIAnnouncements.dbo
{
	public class AnnoncRequest
	{
        public int Number { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
