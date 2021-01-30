using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sunny_dn_01.Domains;
using sunny_dn_01.Service.UserService;
using sunny_dn_01.DataContext;
using sunny_dn_01.Service.KafkaService;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace sunny_dn_01.Controllers
{
    public class VotingController : Controller
    {
        private readonly IMediator _mediator;

        private IKafPublisher _publisher;

        private readonly ILogger<VotingController> _logger;

        public VotingController( IMediator mediator, IKafPublisher kafPublisher, AppDbContext context)
        {
            _mediator = mediator;
            _publisher = kafPublisher;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<VotingModel>>> Votings()
        {
            try
            {
                Console.WriteLine("get Votings");
                List<Voting> votings = await _mediator.Send(new GetVotingsQuery());
                List<VotingModel> models = new List<VotingModel>();
                var groups = votings.GroupBy(v => v.CandidateID);
                foreach (var group in groups)
                {
        
                    models.Add(new VotingModel {
                        CandidateEmail = (await _mediator.Send( new GetUserByIdQuery { UserId = group.First().CandidateID })).Email,
                        VotingCounter= group.Count() - 1});
                   
                }

                return models;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<Voting>> CreateVoting([FromBody] VotingModel votingModel)
        {
            try
            {
                Console.WriteLine("post new voting");
                var resUser = await _mediator.Send(new GetUserByEmailQuery { Email = votingModel.CandidateEmail });
                if (resUser != null)
                {
                    var newVot =await _mediator.Send(new CreateVotingCommand { Voting = new Voting { CandidateID = resUser.ID} });
                    await _publisher.PublishAsync("new-voting", "aaah");
                    return newVot;
                }

                return BadRequest("candidate email is wrong");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        public async Task<string> t03()
        {
            await _publisher.PublishAsync("new-voting", "aaah");
            return "t03";
        }
    }
}
