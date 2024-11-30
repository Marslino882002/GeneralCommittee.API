using GeneralCommittee.Application.Common;
using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Queries.GetAll
{
    public class GetAllMeditationsQuery :IRequest<PageResult<MeditationDto>>
    {
        [MaxLength(100)]
        public string? SearchText { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? sortBy { get; set; }
    }
}
