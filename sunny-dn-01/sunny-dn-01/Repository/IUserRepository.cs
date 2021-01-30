using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using sunny_dn_01.Domains;
namespace sunny_dn_01.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUsersAsync(CancellationToken cancellationToken);
        Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);
        Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
