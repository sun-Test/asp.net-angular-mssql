using System;
namespace sunny_dn_01.Domains
{
    public class VotingModel
    {
        public string CandidateEmail { get; set; }
        public string VoterEmail { get; set; }
        public int VotingCounter { get; set; } = 0;
    }
}
