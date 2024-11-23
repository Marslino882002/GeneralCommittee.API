using GeneralCommittee.Domain.Dtos;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralCommittee.Application.BunnyServices.PodCast.Token;

namespace GeneralCommittee.Application.BunnyServices.PodCast.Get
{
    public class GetPodCastQueryHandler(
    IConfiguration configuration
    ) : IRequestHandler<GetPodCastQuery, PodCastDto>
    {
        public async Task<PodCastDto> Handle(GetPodCastQuery request, CancellationToken cancellationToken)
        {
            var pullZone = configuration["BunnyCdn:PullZone"]!;
            var storageKey = configuration["BunnyCdn:StorageZoneAuthenticationKey"]!;
            var expiryTime = DateTimeOffset.UtcNow.AddHours(4);
            var signedUrl = TokenSigner.SignUrl(t =>
            {
                t.Url = $"https://{pullZone}.b-cdn.net/{request.PodCastId}";
                t.SecurityKey = storageKey;
                t.ExpiresAt = expiryTime;
                t.TokenPath = "/";
            });
            return new PodCastDto()
            {
                Url = signedUrl
            };
        }
    }
}
