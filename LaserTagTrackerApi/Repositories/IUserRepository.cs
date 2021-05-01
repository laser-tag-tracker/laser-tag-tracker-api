using System;
using System.Threading.Tasks;
using LaserTagTrackerApi.Model;

namespace LaserTagTrackerApi.Repositories
{
    public interface IUserRepository
    {
        Task<User> FindByUsername(string username);
        Task<User> CreateUser(User user);
        Task<User> FindUserByIdWithMatches(Guid id);
        Task<User> UpdateUser(User user);
    }
}