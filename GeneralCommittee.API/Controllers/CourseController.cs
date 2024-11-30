using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.Courses.Commands.AddThumbnail;
using GeneralCommittee.Application.Courses.Commands.Create;
using GeneralCommittee.Application.Courses.Commands.DeleteThumbnail;
using GeneralCommittee.Application.Courses.Queries.GetAll;
using GeneralCommittee.Application.Courses.Queries.GetById;
using GeneralCommittee.Application.Videos.Commands.ConfirmUpload;
using GeneralCommittee.Application.Videos.Commands.CreateVideo;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCommittee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(
    IMediator mediator
) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetCourse), new { courseId = result.CourseId }, null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses([FromQuery] GetAllCoursesQuery query)
        {
            var result = await mediator.Send(query);
            var ret = OperationResult<PageResult<CourseViewDto>>.SuccessResult(result);
            return Ok(ret);
        }




        [HttpGet("{courseId}")]
        public async Task<ActionResult<Course>> GetCourse([FromRoute] int courseId)
        {
            var query = new GetCourseByIdQuery { Id = courseId };
            var result = await mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("{courseId}/Thumbnail")]
        public async Task<IActionResult> UpdateThumbnail([FromForm] AddCourseThumbnailCommand command)
        {
            var result = await mediator.Send(command);
            var ret = OperationResult<string>.SuccessResult(result);
            return Ok(ret);
        }

        [HttpDelete("{courseId}/Thumbnail")]
        public async Task<IActionResult> UpdateThumbnail(int courseId)
        {
            var command = new DeleteCourseThumbnailCommand
            {
                CourseId = courseId
            };
            await mediator.Send(command);
            return NoContent();
        }
        [Authorize(AuthenticationSchemes = "Bearer")]

        [HttpPost("{courseId}/Add New Video")]
        public async Task<IActionResult> AddVideo([FromRoute] int courseId, CreateVideoCommand command)
        {
            command.CourseId = courseId;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [Authorize(AuthenticationSchemes = "Bearer")]

        [HttpPost("{courseId}/Confirm Video")]
        public async Task<IActionResult> ConfirmVideo([FromRoute] int courseId, ConfirmUploadCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }




























    }
}
