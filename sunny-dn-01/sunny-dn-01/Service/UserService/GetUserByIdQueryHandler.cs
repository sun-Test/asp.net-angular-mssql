using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using sunny_dn_01.Domains;
using sunny_dn_01.Repository;
using sunny_dn_01.Service.UserService;

namespace sunny_dn_01.Service.UserService
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepo)
        {
            userRepository = userRepo;
        }

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await userRepository.GetUserByIdAsync(request.UserId ,cancellationToken); ;
        }
    }
}
