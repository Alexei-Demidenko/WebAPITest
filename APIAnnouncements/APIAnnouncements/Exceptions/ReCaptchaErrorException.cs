using System;

namespace APIAnnouncements.Exceptions
{
    public class ReCaptchaErrorException : Exception
    {       
        public ReCaptchaErrorException(string message) : base(message)
        {
        }       
    }
}
