using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.Articles.Commands.DeleteArticle;
using GeneralCommittee.Application.Articles.Queries.GetAllArticles;
using GeneralCommittee.Application.Articles.Queries.GetById;
using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.Meditations.Command.AddMeditation;
using GeneralCommittee.Application.Meditations.Command.DeleteMeditation;
using GeneralCommittee.Application.Meditations.Queries.GetAll;
using GeneralCommittee.Application.Meditations.Queries.GetById;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCommittee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeditationController(
        IMediator mediator) : ControllerBase
    {



        [HttpPost("Add New Meditation")]
        public async Task<IActionResult> AddMeditations(AddMeditationCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetMeditation), new { MeditationId = result.MeditationId }, null);
        }




        [HttpDelete("{MeditationId , Title}/Delete Meditation")]
        public async Task<IActionResult> DeleteArticle(int MeditationId, string title)
        {
            var command = new DeleteMeditationCommand
            {
                MeditationId = MeditationId,
                Title = title
            };
            await mediator.Send(command);
            return NoContent();
        }












        [HttpGet("Get All Stored Article")]
        public async Task<IActionResult> GetAllGetMeditations([FromQuery] GetAllMeditationsQuery query)
        {
            var result = await mediator.Send(query);
            var ret = OperationResult<PageResult<MeditationDto>>.SuccessResult(result);
            return Ok(ret);
        }





        [HttpGet("{MeditationId}/Get Meditation by Id")]
        public async Task<ActionResult<Meditation>> GetMeditation([FromRoute] int MeditationId)
        {
            var query = new GetMeditationByIdQuery { Id = MeditationId };
            var result = await mediator.Send(query);
            return Ok(result);
        }




























    }
}
