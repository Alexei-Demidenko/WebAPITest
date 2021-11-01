using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIAnnouncements.Exceptions
{
    public class MaxAnnouncCountException : Exception
    {
        public  MaxAnnouncCountException(string message) : base(message)
        {

        }
    }
}
