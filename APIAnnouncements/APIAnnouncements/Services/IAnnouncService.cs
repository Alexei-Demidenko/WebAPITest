using APIAnnouncements.dbo;
using APIAnnouncements.Utils;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APIAnnouncements.Services
{
	public interface IAnnouncService
	{
		Task<AnnoncResponse> Get(Guid id, CancellationToken cancellationToken);
		Task<DataResult<AnnoncResponse>> GetObjectArray(QueryParameters queryParameters, int page,
			int pageSize, CancellationToken cancellationToken);
		Task Create(CreateAnnoncRequest item, CancellationToken cancellationToken);
		Task Update(Guid id, UpdateAnnoncRequest item, CancellationToken cancellationToken);
		Task Delete(Guid id, CancellationToken cancellationToken);		
	}
}
