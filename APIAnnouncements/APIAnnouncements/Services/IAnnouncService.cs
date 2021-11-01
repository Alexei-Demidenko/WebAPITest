using APIAnnouncements.dbo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace APIAnnouncements.Services
{
	public interface IAnnouncService
	{
		Task<AnnoncResponse> Get(Guid id, CancellationToken cancellationToken);
		Task Create(AnnoncRequest item, CancellationToken cancellationToken);
		Task Update(Guid Id, AnnoncRequest item, CancellationToken cancellationToken);
		Task Delete(Guid Id, CancellationToken cancellationToken);		
	}
}
