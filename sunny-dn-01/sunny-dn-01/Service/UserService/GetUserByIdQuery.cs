using System;
using MediatR;
using sunny_dn_01.Domains;
namespace sunny_dn_01.Service.UserService
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public Guid UserId { get; set; }
    }
}
