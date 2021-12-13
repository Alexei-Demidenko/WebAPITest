using BusinessLayer.Interfaces;
using System;

namespace BusinessLayer.Utils
{
    public class BackgroundDeleteSettings : IBackgroundDeleteSettings
    {
        public TimeSpan Frequency { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
