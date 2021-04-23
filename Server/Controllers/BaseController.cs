using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ResponseCache(Location = ResponseCacheLocation.None, Duration = 0, NoStore = true)]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        /// <summary>
        /// Mediator interface to implement on the other conrollers which inherits from this base class
        /// </summary>
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
    }
}