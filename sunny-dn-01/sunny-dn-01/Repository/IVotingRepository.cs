using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Repository
{
    public interface IVotingRepository : IRepository<Voting>
    {
        Task<List<Voting>> GetVotingsAsync(CancellationToken cancellationToken);
        Task<Voting> GetVotingByUserIdAsync(Guid userId, CancellationToken cancellationToken);    
        
        Task<int> CancelVotingByUserIdAsync(Guid userId, CancellationToken cancellationToken);    
    }
}
