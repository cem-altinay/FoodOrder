using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        [HttpGet("userbyid/{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.User.Queries.GetUsersById.Query() { Id = id }, cancellationToken));
        }

           [HttpGet("allusers")]
        public async Task<IActionResult> GetAllusers( CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new FoodOrder.Application.Features.User.Queries.GetUsers.Query(), cancellationToken));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(FoodOrder.Application.Features.User.Commands.LoginCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(FoodOrder.Application.Features.User.Commands.CreateUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update(FoodOrder.Application.Features.User.Commands.UpdateUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(FoodOrder.Application.Features.User.Commands.DeleteUserCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}