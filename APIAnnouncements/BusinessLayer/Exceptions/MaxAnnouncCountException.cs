using System;

namespace BusinessLayer.Exceptions
{
    public class MaxAnnouncCountException : Exception
    {
        public MaxAnnouncCountException(string message) : base(message)
        {
        }
    }
}
