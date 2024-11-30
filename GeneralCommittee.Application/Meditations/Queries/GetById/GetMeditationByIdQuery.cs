using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Meditations.Queries.GetById
{
    public class GetMeditationByIdQuery : IRequest<MeditationDto>
    {
        public int Id { get; set; }
    }
}
