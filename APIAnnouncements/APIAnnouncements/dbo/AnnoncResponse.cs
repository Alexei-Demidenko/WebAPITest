using APIAnnouncements.Models;
using System;

namespace APIAnnouncements.dbo
{
	public class AnnoncResponse
	{
        public int Number { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; }
        public string Picture { get; set; }
        public int Rating { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
