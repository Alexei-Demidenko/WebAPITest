using BusinessLayer.Interfaces;
using DataAccessLayer.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BackgroundDeleteAnnouncService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IBackgroundDeleteSettings _backgroundDeleteAnnounc;
        public BackgroundDeleteAnnouncService(IBackgroundDeleteSettings backgroundDeleteAnnounc, IServiceProvider serviceProvider)
        {

            _backgroundDeleteAnnounc = backgroundDeleteAnnounc;
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_backgroundDeleteAnnounc.Timeout, stoppingToken);

                using var scope = _serviceProvider.CreateScope();
                var _context = scope.ServiceProvider.GetRequiredService<AnnouncContext>();
                var annoncIsDelete = _context.Announcings.Where(a => (a.ExpirationDate < DateTime.Now.AddDays(-10) && a.IsDeleted == false));
                if (annoncIsDelete != null)
                {
                    foreach (var _annonc in annoncIsDelete)
                    {
                        _annonc.IsDeleted = true;
                    }
                }

                var annoncDelete = _context.Announcings.Where(a => (a.ExpirationDate < DateTime.Now.AddDays(-41) && a.IsDeleted == true));
                if (annoncDelete != null)
                {
                    foreach (var _annonc in annoncDelete)
                    {
                        _context.Announcings.Remove(_annonc);
                    }
                }

                await _context.SaveChangesAsync(stoppingToken);
                await Task.Delay(_backgroundDeleteAnnounc.Frequency, stoppingToken);
            }
        }
        public async override Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
