using AplicationLogic.DTOs.User;
using AplicationLogic.UseCasesInterface.User;
using AplicationLogic.Users.Login;
using BussinesLogic.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLogic.DTOs.User;
using SharedLogic.Exceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketFlowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/<UserController>
        [Authorize (Roles ="Admin,Support")]
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        // GET api/<UserController>/5
        [Authorize(Roles ="Admin,Support")]
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            {
                if (id <= 0) return BadRequest("User not valid");
                GetUserDto user = await _mediator.Send(new GetUserQuery { Id = id});
                return Ok(user);
            }
        }

        // POST api/<UserController>
        
        [HttpPost(Name = "AddUser")]
        public async Task<IActionResult> Post([FromBody] AddUserCommand command)
        {
            if (command == null) return BadRequest("Command not valid");
            var id = await _mediator.Send(command);
            var user = await _mediator.Send(new GetUserQuery { Id = id });
            return CreatedAtAction(
                nameof(Get),
                new { Id=user.Id },
                user
            );
        }

        // PUT api/<UserController>/5
        [Authorize(Roles = "Admin,Support")]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateUserCommand command)
        {
            if (command == null) return BadRequest("Command not valid");
            command.UserId = id;
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<UserController>/5
        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("disable/{id}")]
        public async Task<IActionResult> Disable(int id)
        {
            if (id <= 0) return BadRequest("User not valid");
            var command = new DisableUserCommand
            {
                UserId = id,
                DisableBy = id
            };
            await _mediator.Send(command);
            return NoContent();
        }


        [HttpPost("login")]
        public async Task <IActionResult> Login([FromBody]LoginCommand log)
        {
            if (log == null) return BadRequest("Enter email and password");
           
            var result = await _mediator.Send(log);

            return Ok(result);
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpPost("addComment")]
        public async Task<IActionResult> AddComment(AddUserCommentCommand command)
        {
            if (command == null) return BadRequest("Command not valid");
            await _mediator.Send(command);
            return Created();
        }


    }
}
