using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.VotingService
{
    public class CancelCandidateCommandHandler : IRequestHandler<CancelCandidateCommand, Int32>
    {

        private readonly IVotingRepository _repository;

        public CancelCandidateCommandHandler (IVotingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Int32> Handle(CancelCandidateCommand request, CancellationToken cancellationToken)
        {
            return await _repository.CancelVotingByUserIdAsync(request.UserId, cancellationToken);
        }
    }
}
