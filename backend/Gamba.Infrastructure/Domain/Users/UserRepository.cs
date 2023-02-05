using Gamba.DataAccess.Users;
using Gamba.Infrastructure.Database;
using Gamba.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Gamba.Infrastructure.Domain.Users
{
    public class UserRepository
    {
        private readonly GambaContext _context;

        public UserRepository(GambaContext context)
        {
            _context = context;
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public Task<User> GetById(UserId id)
        {
            return _context.Users
                .IncludePaths(UserEntityTypeConfiguration.FollowingCreatorsList)
                .SingleAsync(x => x.Id == id);
        }
        
        public Task<User> GetByName(string name)
        {
            return _context.Users
                .IncludePaths(UserEntityTypeConfiguration.FollowingCreatorsList)
                .SingleAsync(x => x.Name == name);
        }

        public Task SaveChanges()
        {
            return _context.SaveChangesAsync();
        }
    }
}