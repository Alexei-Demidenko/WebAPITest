using System;

namespace APIAnnouncements.Utils
{
	public class QueryParameters
	{
		public string SearchString { get; set; }
		public Guid? FilterByUserId { get; set; }
		public string SortName { get; set; } = "CreationDate";
		public SortDirection? SortDirection { get; set; } = Utils.SortDirection.Asc;
	}
}
