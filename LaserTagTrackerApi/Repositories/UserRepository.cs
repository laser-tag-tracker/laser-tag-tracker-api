using System;
using System.Linq;
using System.Threading.Tasks;
using LaserTagTrackerApi.Database;
using LaserTagTrackerApi.Model;
using Microsoft.EntityFrameworkCore;

namespace LaserTagTrackerApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<User> usersDbSet;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.usersDbSet = dbContext.Users;
        }

        public Task<User> FindByUsername(string username)
        {
            return usersDbSet
                .Where(user => user.Username == username)
                .FirstAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            usersDbSet.Add(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public Task<User> FindUserByIdWithMatches(Guid id)
        {
            return usersDbSet
                .Include(user => user.Matches)
                .Where(user => user.Id == id)
                .FirstAsync();
        }

        public async Task<User> UpdateUser(User user)
        {
            usersDbSet.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public Task<bool> ExistsByUsername(string username) => usersDbSet
            .AnyAsync(user => user.Username == username);
        
    }
}