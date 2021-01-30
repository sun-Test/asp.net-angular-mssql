using System;
using sunny_dn_01.Domains;
using sunny_dn_01.DataContext;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace sunny_dn_01.Repository
{
    public class VotingRepository : Repository<Voting>, IVotingRepository
    {
        public VotingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Voting> GetVotingByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await dBContext.Votings.FirstOrDefaultAsync(x => x.CandidateID == userId, cancellationToken);
        }

        public async Task<List<Voting>> GetVotingsAsync(CancellationToken cancellationToken)
        {
            return await dBContext.Votings.ToListAsync(cancellationToken);
            //return await dBContext.Votings.Where(x => x.VoterID.Equals(Guid.Empty)).ToListAsync(cancellationToken);
        }
    }
}
