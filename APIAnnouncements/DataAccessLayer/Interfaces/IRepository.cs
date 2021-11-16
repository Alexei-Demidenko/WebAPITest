using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Guid id, CancellationToken cancellationToken);
        Task Create(T item, CancellationToken cancellationToken);
        Task Update(Guid id, T item, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
