using System;
using MediatR;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.UserService
{
    public class CreateUserCommand : IRequest<User>
    {
        public User User { get; set; }
    }
}
