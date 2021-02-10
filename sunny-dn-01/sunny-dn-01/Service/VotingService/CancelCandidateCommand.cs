using System;
using MediatR;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.UserService
{
    public class CancelCandidateCommand : IRequest<int>
    {
        public Guid UserId { get; set; }
    }
}
