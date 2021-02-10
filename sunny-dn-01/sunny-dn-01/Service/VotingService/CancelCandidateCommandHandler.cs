using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.UserService
{
    public class CancelCandidateCommandHandler : IRequestHandler<CancelCandidateCommand, int>
    {

        private readonly IVotingRepository _repository;

        public CancelCandidateCommandHandler (VotingRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CancelCandidateCommand request, CancellationToken cancellationToken)
        {
            return await _repository.CancelVotingByUserIdAsync(request.UserId, cancellationToken);
        }
    }
}
