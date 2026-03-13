using AplicationLogic.DTOs.User;
using AplicationLogic.UseCasesInterface.User;
using BussinesLogic.Exceptions;
using MediatR;
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
        [HttpGet ("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Internal Error");
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id )
        {
            try
            {
                if (id <=0) return BadRequest("Error. Incorrect user data");
                GetUserDto user = await _mediator.Send(new GetUserQuery { Id = id});
                return Ok(user);
            }
            catch
            {
                return StatusCode(500, "Internal error");
            }
        }

        // POST api/<UserController>
        [HttpPost(Name ="AddUser")]
        public async Task<IActionResult> Post([FromBody] AddUserCommand command)
        {
            try
            {
                if (command == null) return BadRequest("Error. Enter correct data and try again.");
                var id = await _mediator.Send(command);
                return CreatedAtRoute("GetUser", new { id }, null);

            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Internal error");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] UpdateUserCommand command)
        {
            try
            {
                if(command == null) return BadRequest("Error. Incorrect user data");
                command.UserId = id;
                await _mediator.Send(command);                
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Internal error");
            }
        }

        // DELETE api/<UserController>/5
        [HttpPut("disable/{id}")]
        public async Task<IActionResult> Disable(int id)
        {
            try
            {
                if (id <= 0) return BadRequest("Error. Incorrect user id");
                var command = new DisableUserCommand
                {
                    UserId = id,
                    DisableBy= id
                };
                await _mediator.Send(command);
                return Ok();
            }
            catch
            {
                return StatusCode(500, "Internal error");
            }
        }
    }
}
