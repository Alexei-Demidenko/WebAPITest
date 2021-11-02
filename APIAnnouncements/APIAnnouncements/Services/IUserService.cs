using APIAnnouncements.dbo;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace APIAnnouncements.Services
{
	public interface IUserService
	{
		Task<UserResponse> Get(Guid id, CancellationToken cancellationToken);
		Task Create(UserRequest item, CancellationToken cancellationToken);
		Task Update(Guid Id, UserRequest item, CancellationToken cancellationToken);
		Task Delete(Guid Id, CancellationToken cancellationToken);
	}
}
