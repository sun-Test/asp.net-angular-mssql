using System;
using System.Collections.Generic;
using MediatR;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.VotingService
{
    public class GetCandidatesWithVotesQuery : IRequest<List<Voting>>
    {
        public GetCandidatesWithVotesQuery()
        {
        }
    }
}
