using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Utils
{
    public class BackgroundDeleteAnnounc : IBackgroundDeleteAnnounc
    {
        private readonly AnnouncContext _context;
        public TimeSpan Frequency { get; set; }
        public TimeSpan Timeout { get; set; }
        public BackgroundDeleteAnnounc()
        {           
            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AsDelete()
        {           
                await Task.Run(() =>
             {
                 Console.WriteLine("Privet");
             });
           
        }
    }
}
