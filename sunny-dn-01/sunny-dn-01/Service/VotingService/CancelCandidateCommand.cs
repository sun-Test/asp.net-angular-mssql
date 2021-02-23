using System;
using MediatR;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.VotingService
{
    public class CancelCandidateCommand : IRequest<Int32>
    {
        public Guid UserId { get; set; }
    }
}
