using System;

namespace APIAnnouncements.Exceptions
{
    public class MaxAnnouncCountException : Exception
    {
        public  MaxAnnouncCountException(string message) : base(message)
        {
        }
    }
}
