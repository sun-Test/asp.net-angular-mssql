using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using sunny_dn_01.DataContext;
using sunny_dn_01.Domains;
namespace sunny_dn_01.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository( AppDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetUsersAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await dBContext.Users.FirstOrDefaultAsync(x => x.ID == userId, cancellationToken);
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await dBContext.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
    }
}
