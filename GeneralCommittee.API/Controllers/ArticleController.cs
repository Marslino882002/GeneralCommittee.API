using GeneralCommittee.Application.Articles.Commands.AddArticle;
using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.Articles.Commands.AddPhotoUrl;
using GeneralCommittee.Application.Articles.Commands.DeleteArticle;
using GeneralCommittee.Application.Articles.Commands.DeletePhotoUrl;
using GeneralCommittee.Application.Articles.Commands.UpdateArticle;
using GeneralCommittee.Application.Articles.Queries.GetAllArticles;
using GeneralCommittee.Application.Articles.Queries.GetById;
using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.Courses.Commands.AddThumbnail;
using GeneralCommittee.Application.Courses.Commands.Create;
using GeneralCommittee.Application.Courses.Commands.DeleteThumbnail;
using GeneralCommittee.Application.Courses.Queries.GetAll;
using GeneralCommittee.Application.Courses.Queries.GetById;
using GeneralCommittee.Domain.Dtos;
using GeneralCommittee.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCommittee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController(
        IMediator mediator) : ControllerBase
    {







        [HttpPost("Add New Article")]
        public async Task<IActionResult> AddArticle(AddArticleCommand command)
        {
            var result = await mediator.Send(command);
            return CreatedAtAction(nameof(GetArticle), new { ArticleId = result.ArticleId }, null);
        }




        [HttpDelete("{ArticleId , title}/Delete Article")]
        public async Task<IActionResult> DeleteArticle(int ArticleId , string title)
        {
            var command = new DeleteArticleCommand
            {
                ArticleId = ArticleId,
                title = title
            };
            await mediator.Send(command);
            return NoContent();
        }







        [HttpGet("Get All Stored Article")]
        public async Task<IActionResult> GetAllArticles([FromQuery] GetAllArticlesQuery query)
        {
            var result = await mediator.Send(query);
            var ret = OperationResult<PageResult<ArticleDto>>.SuccessResult(result);
            return Ok(ret);
        }


 [HttpGet("{ArticleId}/Get Article by Id")]
        public async Task<ActionResult<Article>> GetArticle([FromRoute] int ArticleId)
        {
            var query = new GetArticleByIdQuery { Id = ArticleId };
            var result = await mediator.Send(query);
            return Ok(result);
        }



        [HttpPost("{ArticleId}/Update Content of Article")]
        public async Task<IActionResult> UpdateArticle([FromForm] UpdateArticleCommand command)
        {
            var result = await mediator.Send(command);
            var ret = OperationResult<string>.SuccessResult(result);
            return Ok(ret);
        }




        [HttpPost("{courseId}/Add Photo")]
        public async Task<IActionResult> UpdatePhotoUrl([FromForm] AddPhotoUrlCommand command)
        {
            var result = await mediator.Send(command);
            var ret = OperationResult<string>.SuccessResult(result);
            return Ok(ret);
        }


        [HttpDelete("{courseId}/Delete Photo")]
        public async Task<IActionResult> DeletePhotoUrl(int ArticleId)
        {
            var command = new DeletePhotoUrlCommand
            {
                ArticleId = ArticleId
            };
            await mediator.Send(command);
            return NoContent();
        }







    }
}
