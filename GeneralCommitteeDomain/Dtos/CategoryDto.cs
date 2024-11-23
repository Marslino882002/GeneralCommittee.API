using GeneralCommittee.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(Global.MaxNameLength)]
        public string Name { get; set; } = default!;
        //todo add max length
        public string? Description { get; set; }
    }
}
