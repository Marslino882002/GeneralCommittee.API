using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Dtos
{
    public class MeditationDto
    {


        public int MeditationId { get; set; }

        //todo implement it with url to avoid heavy db searches
        public string Content { get; set; } = default!;





    }
}
