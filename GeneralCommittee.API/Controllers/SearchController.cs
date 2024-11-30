using GeneralCommittee.Application.Searching.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCommittee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController  (IMediator _mediator): ControllerBase
    {


        [HttpGet("search")] 
        public async Task<IActionResult> Search([FromQuery] string searchTerm,
    [FromQuery] string materialType, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        { 
            var query = new SearchMaterialQuery 
            { SearchTerm = searchTerm, MaterialType = materialType,
                PageNumber = pageNumber, PageSize = pageSize };
            var results = await _mediator.Send(query); return Ok(results); }


    }
}
