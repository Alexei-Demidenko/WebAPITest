using BusinessLayer.DataTransferObject.AnnoncDTO;
using BusinessLayer.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IAnnouncService
    {
        Task<AnnoncResponseDto> Get(Guid id, CancellationToken cancellationToken);
        Task<DataResult<AnnoncResponseDto>> GetObjectArray(QueryParameters queryParameters, int page,
            int pageSize, CancellationToken cancellationToken);
        Task Create(AnnoncCreateRequestDto item, CancellationToken cancellationToken);
        Task Update(Guid id, AnnoncUpdateRequestDto item, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
