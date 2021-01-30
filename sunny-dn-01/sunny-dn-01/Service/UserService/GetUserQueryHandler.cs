using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;

namespace sunny_dn_01.Service.UserService
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, List<User>>
    {
        private readonly IUserRepository userRepository;

        public GetUserQueryHandler(IUserRepository userRepo)
        {
            userRepository = userRepo;
        }

        public async Task<List<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUsersAsync(cancellationToken); ;
        }
    }
}
