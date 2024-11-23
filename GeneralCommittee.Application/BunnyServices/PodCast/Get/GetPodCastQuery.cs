using GeneralCommittee.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.PodCast.Get
{
    public class GetPodCastQuery : IRequest<PodCastDto>
    {
        public string PodCastId { set; get; } = default!;
    }
}
