using BusinessLayer.DataTransferObject.UserDTO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> Get(Guid id, CancellationToken cancellationToken);
        Task Create(UserRequestDto item, CancellationToken cancellationToken);
        Task Update(Guid id, UserRequestDto item, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }
}
