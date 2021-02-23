using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.VotingService
{
    public class CreateVotingCommandHandler : IRequestHandler<CreateVotingCommand, Voting>
    {
        private readonly IVotingRepository _repository;

        public CreateVotingCommandHandler(IVotingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Voting> Handle(CreateVotingCommand request, CancellationToken cancellationToken)
        {
            return await _repository.AddAsync(request.Voting);
        }
    }
}
