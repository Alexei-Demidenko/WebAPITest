using BusinessLayer.Interfaces;
using System;
using System.Data;
using Microsoft.Extensions.Options;
using DataAccessLayer.Context;
using AutoMapper;
using BusinessLayer.Options;
using System.Threading.Tasks;
using System.Threading;
using BusinessLayer.DataTransferObject.AnnoncDTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Utils;
using DataAccessLayer.Models;
using BusinessLayer.Extensions;
using BusinessLayer.Exceptions;

namespace BusinessLayer.Services
{
    public class AnnouncService : IAnnouncService
    {
        private readonly AnnouncContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<AnnouncOption> _maxAnnouncCountOption;
        public AnnouncService(AnnouncContext context, IMapper mapper, IOptions<AnnouncOption> maxAnnouncCountOption)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _maxAnnouncCountOption = maxAnnouncCountOption ?? throw new ArgumentNullException(nameof(maxAnnouncCountOption));
        }
        public async Task<AnnoncResponseDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var announcingdb = await _context.Announcings.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            var item = _mapper.Map<AnnoncResponseDto>(announcingdb);
            return item;
        }
        public async Task<DataResult<AnnoncResponseDto>> GetObjectArray(QueryParameters queryParameters, int page, int pageSize, CancellationToken cancellationToken)
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

            var pagedResult = await announcingQuery.GetPaged<AnnoncResponseDto, Announcing>(page, pageSize, _mapper, cancellationToken);

            var result = new DataResult<AnnoncResponseDto>
            {
                Data = pagedResult.Result,
                Count = pagedResult.RowCount
            };

            return result;
        }
        public async Task Create(AnnoncCreateRequestDto item, CancellationToken cancellationToken)
        {
            await using (var transaction = _context.Database.BeginTransaction(IsolationLevel.Serializable))
            {
                try
                {
                    if (_context.Users.Where(a => a.Id == item.UserId) == null)
                    {
                        throw new NotExistUsertException("Не сущесвует пользователя с таким ID.");
                    }

                    if (_context.Set<Announcing>().Count(a => a.User.Id == item.UserId) >
                        _maxAnnouncCountOption.Value.MaxAnnouncCount)
                    {
                        throw new MaxAnnouncCountException("Достигнуто максимальное количество объявлений!");
                    }

                    var announcingdb = _mapper.Map<Announcing>(item);
                    announcingdb.Number = _context.Set<Announcing>().Count() + 1;
                    announcingdb.Id = Guid.NewGuid();
                    announcingdb.CreationDate = DateTime.Now;
                    announcingdb.ExpirationDate = DateTime.Now.AddDays(10);
                    announcingdb.UserId = item.UserId;

                    await _context.Announcings.AddAsync(announcingdb);
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (NotExistUsertException)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
                catch (MaxAnnouncCountException)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync(cancellationToken);
                }
            }
        }
        public async Task Update(Guid id, AnnoncUpdateRequestDto updatedAnnouncing, CancellationToken cancellationToken)
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
