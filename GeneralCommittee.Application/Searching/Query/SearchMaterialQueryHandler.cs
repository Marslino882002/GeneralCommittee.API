using GeneralCommittee.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Searching.Query
{
    public class SearchMaterialQueryHandler(
        ISearchServiceRepository<object> searchServiceRepository)
        : IRequestHandler<SearchMaterialQuery, IEnumerable<object>>
    {
        public async Task<IEnumerable<object>> Handle(SearchMaterialQuery request, CancellationToken cancellationToken)
        {



            return (IEnumerable<object>)await 
                searchServiceRepository
                .SearchAsync(request.SearchTerm, request.MaterialType, request.PageNumber, request.PageSize);





        }
    }
}
