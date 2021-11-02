using System;

namespace APIAnnouncements.Exceptions
{
	public class EntityNotFoundException : Exception
	{
		public EntityNotFoundException(string message) :base (message)
		{			
		}
	}
}
