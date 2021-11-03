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

namespace APIAnnouncements.Services
{
    public class UserService : IUserService
    {
        private readonly AnnouncementsContext _context;
        private readonly IMapper _mapper;
		public UserService(AnnouncementsContext context,IMapper mapper)
		{
            _context = context?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }        
        public async Task<UserResponse> Get(Guid id, CancellationToken cancellationToken)
        {
            var userdb = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync(cancellationToken);
            var item = _mapper.Map<UserResponse>(userdb);
            return item;
        }        
        public async Task Create(UserRequest item, CancellationToken cancellationToken)
        {
            var userdb= _mapper.Map<User>(item);
            userdb.Id = Guid.NewGuid();
            userdb.CreationDate = DateTime.Now;

            _context.Users.Add(userdb);
            await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task Update(Guid Id, UserRequest updatedUser, CancellationToken cancellationToken)
        {
             // User currentItem = Get(updatedUser.Id);
            var userdb = await _context.Users.Where(u => u.Id == Id).FirstOrDefaultAsync(cancellationToken);
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
