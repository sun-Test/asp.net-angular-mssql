using System;
using System.Collections.Generic;
using sunny_dn_01.Domains;
using MediatR;

namespace sunny_dn_01.Service.UserService
{
    public class GetUserQuery : IRequest<List<User>>
    {
        public GetUserQuery()
        {
        }
    }
}
