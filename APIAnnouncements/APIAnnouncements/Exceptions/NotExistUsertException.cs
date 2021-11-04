using System;

namespace APIAnnouncements.Exceptions
{
    public class NotExistUsertException : Exception
    {
        public NotExistUsertException(string message) : base(message)
        {
        }
    }
}
