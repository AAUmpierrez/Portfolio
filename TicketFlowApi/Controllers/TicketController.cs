using AplicationLogic.DTOs.Ticket;
using AplicationLogic.DTOs.User;
using AplicationLogic.Tickets.ChangeState.AssignTicket;
using AplicationLogic.Tickets.ChangeState.CloseTicket;
using AplicationLogic.Tickets.ChangeState.InProcessTicket;
using AplicationLogic.Tickets.ChangeState.ReopenTicket;
using AplicationLogic.Tickets.ChangeState.ResolveTicket;
using AplicationLogic.Tickets.ChangeState.WaitingTicket;
using AplicationLogic.Tickets.CommentList;
using AplicationLogic.Tickets.Dashboard;
using AplicationLogic.Tickets.GetMyTickets;
using AplicationLogic.Tickets.RemoveTicketComment;
using AplicationLogic.Tickets.Ticketinterf;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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


        [Authorize(Roles = "Admin,Support")]
        [HttpGet("tickets")]
        public async Task<IActionResult> GetAll([FromQuery]GetAllTicketQuery query)
        {
            if (query == null) return BadRequest("Query not valid");
            var tickets= await _mediator.Send(query);
            return Ok(tickets);
        }
        [Authorize(Roles = "Admin,Support,Client")]
        [HttpGet("mytickets")]
        public async Task<IActionResult> GetMyTickets([FromQuery] GetMyTicketsQuery query)
        {
            if (query == null) return BadRequest("Query not valid");
            query.CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }

        // GET api/<TicketController>/5
        [Authorize(Roles = "Admin,Support,Client")]
        [HttpGet("ticket/{id}",Name ="GetTicketById")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0) return BadRequest("Ticket id not valid");
            var ticket = await _mediator.Send(new GetTicketQuery {Id = id });
            return Ok(ticket);
        }

        // POST api/<TicketController>
        [Authorize(Roles = "Admin,Support,Client")]
        [HttpPost("AddTicket")]
        public async Task<IActionResult> Post([FromBody] AddTicketDto dto )
        {
            if (dto == null) return BadRequest("Command not valid");
            var command = new AddTicketCommand
            {
                Title = dto.Title,
                Description = dto.Description,
                Priority = dto.Priority
            };
            command.CreatorUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            int id = await _mediator.Send(command);
            var ticket = await _mediator.Send(new GetTicketQuery { Id = id });
            return CreatedAtAction(
                    nameof(Get),
                    new { id = id },
                    ticket
                );
        }


        [Authorize(Roles = "Admin,Support,Client")]
        [HttpPost("addComment/{id}")]
        public async Task<IActionResult> AddComment([FromForm] AddTicketCommentCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            command.TicketId = id;
            command.CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            command.Role = User.FindFirstValue(ClaimTypes.Role.ToString());
            await _mediator.Send(command);
            return Created();
        }

        [Authorize(Roles = "Admin,Support,client")]
        [HttpPatch("removeTicketComment/{id}")]
        public async Task<IActionResult> ChangePriority([FromBody] RemoveTicketCommentCommand command, int id)
        {
           if(command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Support,Client")]
        [HttpGet("comments/ticket/{id}")]
        public async Task<IActionResult> GetTicketComments(int id)
        {
            if (id <= 0) return BadRequest("Ticket not valid");
            var comments = await _mediator.Send(new TicketCommentListQuery { TicketId = id });
            return Ok(comments);
        }



        [Authorize(Roles = "Admin,Support")]
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var dashboard = await _mediator.Send(new GetTicketDashboardQuery());
            return Ok(dashboard);
        }


        // PUT api/<TicketController>/5
        [Authorize(Roles = "Admin,Support,Client")]
        [HttpPut("update/ticket/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateTicketCommand comand)
        {
            if (comand == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            comand.TicketId = id;
            await _mediator.Send(comand);
            return NoContent();
        }

        //// DELETE api/<TicketController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        //Disable
        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("sDelete/{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            if (id <= 0) return BadRequest("Ticket not valid");
            DeleteTicketCommand command = new DeleteTicketCommand
            {
                TicketId = id,
                UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            await _mediator.Send(command);
            return NoContent();
        }


        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("assignTicket/{id}")]
        public async Task<IActionResult> AssignTicket([FromBody]AssignTicketCommand command, int id)
         {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.AssignedByUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("processTicket/{id}")]
        public async Task<IActionResult> InProccess([FromBody] ProcessTicketCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("waitingTicket/{id}")]
        public async Task<IActionResult> Waiting([FromBody] WaitingTicketCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("resolveTicket/{id}")]
        public async Task<IActionResult> Resolve([FromBody] ResolveTicketCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("reopenTicket/{id}")]
        public async Task<IActionResult> Reopen([FromBody] ReopenTicketCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Support,Client")]
        [HttpPatch("closeTicket/{id}")]
        public async Task<IActionResult> Close([FromBody] CloseTicketCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.UserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }


        [Authorize(Roles = "Admin,Support")]
        [HttpPatch("changePriority/{id}")]
        public async Task<IActionResult> ChangePriority([FromBody] ChangePriorityCommand command, int id)
        {
            if (command == null) return BadRequest("Command not valid");
            if (id <= 0) return BadRequest("Ticket not valid");
            command.TicketId = id;
            command.CurrentUser = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _mediator.Send(command);
            return NoContent();
        }






    }
}
