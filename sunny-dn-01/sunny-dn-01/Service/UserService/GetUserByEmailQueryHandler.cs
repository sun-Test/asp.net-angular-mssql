using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.UserService
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, User>
    {
        private readonly IUserRepository userRepository;

        public GetUserByEmailQueryHandler(IUserRepository userRepo)
        {
            userRepository = userRepo;
        }

        public async Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
        }
    }
}
