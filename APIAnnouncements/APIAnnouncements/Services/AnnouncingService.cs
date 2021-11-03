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
using APIAnnouncements.Utils;
using APIAnnouncements.Extensions;

namespace APIAnnouncements.Services
{
    public class AnnouncingService : IAnnouncService
    {
        private readonly AnnouncementsContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<MaxAnnouncCountOption> _maxAnnouncCountOption;
        public AnnouncingService(AnnouncementsContext context, IMapper mapper, IOptions<MaxAnnouncCountOption> maxAnnouncCountOption)
		{
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _maxAnnouncCountOption = maxAnnouncCountOption ?? throw new ArgumentNullException(nameof(maxAnnouncCountOption));
        }
       
        public async Task<AnnoncResponse> Get(Guid id, CancellationToken cancellationToken)
        {
            var announcingdb = await _context.Announcings.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            var item = _mapper.Map<AnnoncResponse>(announcingdb);
            return item;
        }
        public async Task<DataResult<AnnoncResponse>> GetObjectArray(QueryParameters queryParameters, int page, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Announcing> announcingQuery = _context.Set<Announcing>();

            if (!string.IsNullOrEmpty(queryParameters.SearchString))
            {
                announcingQuery = announcingQuery.SearchForMatches(queryParameters.SearchString);
            }

            if (queryParameters.FilterByUserId != null)
            {
                announcingQuery = announcingQuery.Where(w => w.User.Id == queryParameters.FilterByUserId);
            }

            if ("number".StartsWith(queryParameters.SortName.ToLower()))
            {
                announcingQuery = announcingQuery.GetSortBy(x => x.Number, queryParameters.SortDirection);
            }
            else if ("rating".StartsWith(queryParameters.SortName.ToLower()))
            {
                announcingQuery = announcingQuery.GetSortBy(x => x.Rating, queryParameters.SortDirection);
            }
            else announcingQuery = announcingQuery.GetSortBy(x => x.CreationDate, queryParameters.SortDirection);

            var pagedResult = await announcingQuery.GetPaged<AnnoncResponse, Announcing>(page, pageSize, _mapper, cancellationToken);

            var result = new DataResult<AnnoncResponse>
            {
                Data = pagedResult.Result,
                Count = pagedResult.RowCount
            };

            return result;
        }
        public async Task Create(AnnoncRequest item, CancellationToken cancellationToken)
        {
            if (_context.Set<Announcing>().Where(a => a.User.Id == item.User.Id).Count() < _maxAnnouncCountOption.Value.MaxAnnouncCount)
            {
                var announcingdb = _mapper.Map<Announcing>(item);
                announcingdb.Id = Guid.NewGuid();

                _context.Announcings.Add(announcingdb);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new MaxAnnouncCountException("Достигнуто максимальное количество объявлений!");
            }
        }
        public async Task Update(Guid id, AnnoncRequest updatedAnnouncing, CancellationToken cancellationToken)
        { 
            var announcingdb = await _context.Announcings.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (announcingdb == null)
                throw new EntityNotFoundException(nameof(announcingdb));
            _mapper.Map(updatedAnnouncing, announcingdb);


            _context.Announcings.Update(announcingdb);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var announcingdb = await _context.Announcings.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (announcingdb == null)
                throw new EntityNotFoundException(nameof(announcingdb));

            _context.Announcings.Remove(announcingdb);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
