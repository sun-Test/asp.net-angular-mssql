using System;
namespace sunny_dn_01.Domains
{
    public class Voting
    {
        public Guid ID { get; set; }
        public Guid CandidateID { get; set; }
        public Guid VoterID { get; set; }
    }
}
