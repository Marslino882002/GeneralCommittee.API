using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Dtos
{
    public class AddVideoDto
    {
        public int CourseId { get; set; }
        public string? Description { set; get; }
        public string Title { set; get; } = default!;
    }
}
