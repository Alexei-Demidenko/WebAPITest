using System;

namespace BusinessLayer.Interfaces
{
    public interface IBackgroundDeleteSettings
    {
        TimeSpan Frequency { get; set; }
        TimeSpan Timeout { get; set; }
    }
}
