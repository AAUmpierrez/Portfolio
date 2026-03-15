using AplicationLogic.DTOs.User;
using AplicationLogic.Tickets.ChangeState.AssignTicket;
using AplicationLogic.Tickets.Ticketinterf;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedLogic.DTOs.Ticket;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TicketFlowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController (IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<TicketController>
        [Authorize(Roles = "Admin,Support")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromBody]GetAllTicketQuery query)
        {
            if (query == null) return BadRequest("Query not valid");
            var tickets= await _mediator.Send(query);
            return Ok(tickets);
        }

        // GET api/<TicketController>/5
        [Authorize(Roles = "Admin,Support")]
        [HttpGet("ticket/{id}",Name ="GetTicketById")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return BadRequest("Ticket id not valid");
            var ticket = await _mediator.Send(new GetUserDto {Id = id });
            return Ok(ticket);
        }

        // POST api/<TicketController>
        [Authorize(Roles = "Admin,Support")]
        [HttpPost("AddTicket")]
        public async Task<IActionResult> Post([FromBody] AddTicketCommand command )
        {
            if (command == null) return BadRequest("Command not valid");
            int id = await _mediator.Send(command);
            return CreatedAtRoute("GetTicketById", new { Id = id},null);
        }

        // PUT api/<TicketController>/5
        [Authorize(Roles ="Admin,Support")]
        [HttpPut("update/ticket/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTicketCommand comand)
        {
            if (comand == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            comand.Id = id;
            await _mediator.Send(comand);
            return Ok($"{comand.Title} updated");
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //Disable
        [Authorize(Roles ="Admin,Support")]
        [HttpPut("sDelete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            if (id <= 0) return BadRequest("Ticket not valid");
            DeleteTicketCommand command = new DeleteTicketCommand
            {
                TicketId = id,
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            await _mediator.Send(command);
            return Ok();
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpPost("changeState/ticket/{id}")]
        public async Task<IActionResult> AssignTicket([FromBody]AssignTicketCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return Ok();
        }
    }
}
