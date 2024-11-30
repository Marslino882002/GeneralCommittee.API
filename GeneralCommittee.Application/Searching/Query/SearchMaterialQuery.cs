using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Searching.Query
{
    public class SearchMaterialQuery : IRequest<IEnumerable<object>>
    {


        public string SearchTerm { get; set; }
        public string MaterialType { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }










    }
}
