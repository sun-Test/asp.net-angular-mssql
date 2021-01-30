﻿using System;
using System.Collections.Generic;
using MediatR;
using sunny_dn_01.Domains;

namespace sunny_dn_01.Service.UserService
{
    public class GetVotingCandidatesQuery : IRequest<List<Voting>>
    {
        public GetVotingCandidatesQuery()
        {
        }
    }
}
