using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Services.BackgroundTasks
{
    public class BackgroundDeleteAnnouncService : BackgroundService
    {
        private readonly AnnouncContext _context;
        private readonly IBackgroundDeleteAnnounc BackgroundDeleteAnnounc;
        public BackgroundDeleteAnnouncService(IBackgroundDeleteAnnounc backgroundDeleteAnnounc)
        {
           
            BackgroundDeleteAnnounc = backgroundDeleteAnnounc;
            //_context = context ?? throw new ArgumentNullException(nameof(context));
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(BackgroundDeleteAnnounc.Timeout, stoppingToken);

                await BackgroundDeleteAnnounc.AsDelete();

                await Task.Delay(BackgroundDeleteAnnounc.Frequency, stoppingToken);
            }
        }
        public async override Task StopAsync(CancellationToken cancellationToken)
        {          
            await base.StopAsync(cancellationToken);
        }
    }
}
