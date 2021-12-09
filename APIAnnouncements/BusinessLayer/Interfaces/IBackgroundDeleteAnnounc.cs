using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBackgroundDeleteAnnounc
    {
        TimeSpan Frequency { get; set; }
        TimeSpan Timeout { get; set; }
        Task AsDelete();
    }
}
