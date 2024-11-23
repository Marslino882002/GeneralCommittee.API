using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Dtos
{
    public class InstructorDto
    {
        public int InstructorId { get; set; }
        public string Name { get; set; } = default!;
        public string? About { get; set; }
        public string? ImageUrl { get; set; }
    }
}
