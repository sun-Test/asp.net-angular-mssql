using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sunny_dn_01.Domains;
using MediatR;
using sunny_dn_01.Service.UserService;
using sunny_dn_01.Service.KafkaService;
using sunny_dn_01.Service.VotingService;

namespace sunny_dn_01.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        private readonly IMediator _mediator;

        private IKafPublisher _publisher;

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger, IMediator mediator, IKafPublisher publisher)
        {
            _logger = logger;
            _mediator = mediator;
            _publisher = publisher;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<Voting>>> Votings()
        {
            try
            {
                Console.WriteLine("get Votings");
                return await _mediator.Send(new GetVotingsQuery());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet]
        public async Task<string> t03()
        {
            await _publisher.PublishAsync("new-user", "aaah");
            return "t03";
        }






        /*
        public void cacheTest()
        {
            string cVal;
            if(_cache.TryGetValue("myUniqueId", out cVal))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(9));
                _cache.Set("myUniqueId", cVal, cacheEntryOptions);
            }
        }
        */
    }
}
