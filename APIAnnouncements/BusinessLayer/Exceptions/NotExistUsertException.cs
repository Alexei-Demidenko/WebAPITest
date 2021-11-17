using System;

namespace BusinessLayer.Exceptions
{
    public class NotExistUsertException : Exception
    {
        public NotExistUsertException(string message) : base(message)
        {
        }
    }
}
