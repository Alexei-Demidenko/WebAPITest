using AutoMapper;
using System;
using DataAccessLayer.Context;
using BusinessLayer.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using BusinessLayer.DataTransferObject.UserDTO;
using System.Linq;
using BusinessLayer.Exceptions;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly AnnouncContext _context;
        private readonly IMapper _mapper;
        public UserService(AnnouncContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<UserResponseDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var userdb = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            var item = _mapper.Map<UserResponseDto>(userdb);
            return item;
        }
        public async Task Create(UserRequestDto item, CancellationToken cancellationToken)
        {
            var userdb = _mapper.Map<User>(item);
            userdb.Id = Guid.NewGuid();
            userdb.CreationDate = DateTime.Now;

            _context.Users.Add(userdb);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(Guid id, UserRequestDto updatedUser, CancellationToken cancellationToken)
        {
            // User currentItem = Get(updatedUser.Id);
            var userdb = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (userdb == null)
                throw new EntityNotFoundException(nameof(userdb));
            _mapper.Map(updatedUser, userdb);

            _context.Users.Update(userdb);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Delete(Guid id, CancellationToken cancellationToken)
        {
            var userdb = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            if (userdb == null)
                throw new EntityNotFoundException(nameof(userdb));

            _context.Users.Remove(userdb);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
