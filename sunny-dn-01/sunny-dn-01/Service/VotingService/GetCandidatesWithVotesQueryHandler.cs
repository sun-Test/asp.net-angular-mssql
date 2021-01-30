using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.VotingService
{
    public class GetCandidatesWithVotesQueryHandler : IRequestHandler<GetCandidatesWithVotesQuery, List<Voting>>
    {
        public GetCandidatesWithVotesQueryHandler()
        {
        }

        public Task<List<Voting>> Handle(GetCandidatesWithVotesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
