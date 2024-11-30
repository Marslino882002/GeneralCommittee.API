using GeneralCommittee.Application.BunnyServices.VideoContent.Collection.Add;
using GeneralCommittee.Application.Videos.Commands.CreateVideo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCommittee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TryController(
    IMediator mediator
) : ControllerBase
    {


        [HttpPost]
        [Route("checkInfo")]
        public async Task<IActionResult> ResendConfirmation([FromBody] AddCollectionCommand command)
        {
            var commandResult = await mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpGet]
        [Route("any")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Try()
        {
            return Ok("You're Authorized");
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(CreateVideoCommand command)
        {
            var commandResult = await mediator.Send(command);
            return Ok(commandResult);
        }




















    }
}
