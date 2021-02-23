using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.VotingService
{
    public class GetVotingsHandler : IRequestHandler<GetVotingsQuery, List<Voting>>
    {
        private readonly IVotingRepository repository;

        public GetVotingsHandler(IVotingRepository repo)
        {
            repository = repo;
        }

        public async Task<List<Voting>> Handle(GetVotingsQuery request, CancellationToken cancellationToken)
        {
            return await repository.GetVotingsAsync(cancellationToken);
        }
    }
}
