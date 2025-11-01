using HajorPay.ThriftService.Application.Features.CreateGroup;
using HajorPay.ThriftService.Application.Features.GetGroupById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HajorPay.ThriftService.API.Controllers
{
    [Route("api/groups")]
    [ApiController]
    public class GroupsController : BaseController
    {
        private readonly ISender _sender;
        public GroupsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateGroup(CreateGroupCommand command)
        {
            await _sender.Send(command);
            return Created();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById(Guid id)
        {
            var group = await _sender.Send(new GetGroupByIdQuery(id));
            return (group is null) ? NotFound() : Ok(group);
        }
    }
}
