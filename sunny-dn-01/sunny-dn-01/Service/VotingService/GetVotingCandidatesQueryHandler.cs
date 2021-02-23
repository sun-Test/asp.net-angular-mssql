using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;


namespace sunny_dn_01.Service.VotingService
{
    public class GetVotingCandidatesQueryHandler : IRequestHandler<GetVotingCandidatesQuery, List<Voting>>
    {
        private readonly IVotingRepository _repository;

        public GetVotingCandidatesQueryHandler(IVotingRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Voting>> Handle(GetVotingCandidatesQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetVotingsAsync(cancellationToken);
        }
    }
}
