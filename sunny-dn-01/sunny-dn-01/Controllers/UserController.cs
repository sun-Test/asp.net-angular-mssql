using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using sunny_dn_01.Domains;
using sunny_dn_01.Service.UserService;

using sunny_dn_01.Service.KafkaService;
using sunny_dn_01.DataContext;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace sunny_dn_01.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private IKafPublisher _publisher;


        public UserController(IMapper mapper, IMediator mediator, IKafPublisher kafPublisher, AppDbContext context)
        {
            _mapper = mapper;
            _mediator = mediator;
            _publisher = kafPublisher;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Users()
        {
            try
            {
                Console.WriteLine("getAllUsers");
                return await _mediator.Send(new GetUserQuery());
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
        public async Task<ActionResult<User>> NewUser([FromBody] User user)
        {
            try
            {
                var resUser = await _mediator.Send(new GetUserByEmailQuery { Email = user.Email });
                if (resUser == null)
                {
                    var newUser = await _mediator.Send(new CreateUserCommand
                                    {
                                        User = user
                                    });

                    await _publisher.PublishAsync("new-user", JsonSerializer.Serialize( newUser.Email));
                    return newUser;
                    
                }

                return resUser;
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<User>> Login([FromBody] User user)
        {
            try
            {
                var resUser = await _mediator.Send(new GetUserByEmailQuery { Email = user.Email });
                if (resUser == null)
                {
                    return Unauthorized(); ;

                }
                return user;

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<string> t01()
        {
            await _publisher.PublishAsync("new-user", "123@456.com");
            return "t01";
        }

        [HttpGet]
        public async Task<ActionResult<User>> t02()
        {
            try
            {
                return await _mediator.Send(new CreateUserCommand
                {
                    User = new User { Email = "123@456.com" }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
