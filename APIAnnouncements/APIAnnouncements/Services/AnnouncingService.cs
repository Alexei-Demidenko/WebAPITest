using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIAnnouncements.Models;
using APIAnnouncements.Context;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using APIAnnouncements.Exceptions;
using APIAnnouncements.dbo;
using AutoMapper;


namespace APIAnnouncements.Services
{
    public class AnnouncingService : IAnnouncService
    {
        private AnnouncementsContext _context;
        private IMapper _mapper;
		public AnnouncingService(AnnouncementsContext context, IMapper mapper)
		{
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
       
        public async Task<AnnoncResponse> Get(Guid Id, CancellationToken cancellationToken)
        {
            Announcing Announcingdb = await _context.Announcings.Where(u => u.Id == Id).FirstOrDefaultAsync(cancellationToken);
            AnnoncResponse item = _mapper.Map<AnnoncResponse>(Announcingdb);
            return item;
        }
       
        public async Task Create(AnnoncRequest item, CancellationToken cancellationToken)
        {
            Announcing announcingdb = _mapper.Map<Announcing>(item);
            announcingdb.Id = Guid.NewGuid();

            _context.Announcings.Add(announcingdb);
            await _context.SaveChangesAsync(cancellationToken);
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
