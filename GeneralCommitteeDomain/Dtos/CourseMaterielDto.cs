using GeneralCommittee.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Domain.Dtos
{
    public class CourseMaterielDto
    {
        public int CourseMaterielId { get; set; }

        [MaxLength(Global.TitleMaxLength)]
        public string? Title { set; get; }

        //todo add max length to description 
        public string? Description { set; get; }

        //
        public int ItemOrder { get; set; }

        [MaxLength(Global.UrlMaxLength)]
        public string Url { set; get; } = default!;
        public bool IsVideo { set; get; }
    }
}
