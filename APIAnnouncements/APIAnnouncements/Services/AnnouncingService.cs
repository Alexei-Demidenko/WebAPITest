using System;
using System.Linq;
using System.Threading.Tasks;
using APIAnnouncements.Models;
using APIAnnouncements.Context;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using APIAnnouncements.Exceptions;
using APIAnnouncements.dbo;
using AutoMapper;
using Microsoft.Extensions.Options;
using APIAnnouncements.Options;

namespace APIAnnouncements.Services
{
    public class AnnouncingService : IAnnouncService
    {
        private AnnouncementsContext _context;
        private IMapper _mapper;
        private readonly IOptions<MaxAnnouncCountOption> _maxAnnouncCountOption;
        public AnnouncingService(AnnouncementsContext context, IMapper mapper, IOptions<MaxAnnouncCountOption> maxAnnouncCountOption)
		{
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _maxAnnouncCountOption = maxAnnouncCountOption ?? throw new ArgumentNullException(nameof(maxAnnouncCountOption));
        }
       
        public async Task<AnnoncResponse> Get(Guid Id, CancellationToken cancellationToken)
        {
            Announcing Announcingdb = await _context.Announcings.Where(u => u.Id == Id).FirstOrDefaultAsync(cancellationToken);
            AnnoncResponse item = _mapper.Map<AnnoncResponse>(Announcingdb);
            return item;
        }
       
        public async Task Create(AnnoncRequest item, CancellationToken cancellationToken)
        {
            if (_context.Set<Announcing>().Where(a => a.User.Id == item.User.Id).Count() < _maxAnnouncCountOption.Value.MaxAnnouncCount)
            {
                Announcing announcingdb = _mapper.Map<Announcing>(item);
                announcingdb.Id = Guid.NewGuid();

                _context.Announcings.Add(announcingdb);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new MaxAnnouncCountException("Достигнуто максимальное количество объявлений!");
            }
        }
        public async Task Update(Guid Id, AnnoncRequest updatedAnnouncing, CancellationToken cancellationToken)
        { 
            Announcing announcingdb = await _context.Announcings.Where(u => u.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (announcingdb == null)
                throw new EntityNotFoundException(nameof(announcingdb));
            _mapper.Map(updatedAnnouncing, announcingdb);


            _context.Announcings.Update(announcingdb);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid Id, CancellationToken cancellationToken)
        {
            Announcing announcingdb = await _context.Announcings.Where(u => u.Id == Id).FirstOrDefaultAsync(cancellationToken);
            if (announcingdb == null)
                throw new EntityNotFoundException(nameof(announcingdb));

            _context.Announcings.Remove(announcingdb);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
