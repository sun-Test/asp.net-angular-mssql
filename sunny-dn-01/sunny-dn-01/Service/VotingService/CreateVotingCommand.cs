using System;
using MediatR;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.VotingService
{
    public class CreateVotingCommand : IRequest<Voting>
    {
        public Voting Voting { get; set; }
    }
}
