using GeneralCommittee.Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Video.CreateVideo
{
    public class CreateVideoCommandHandler(
    IConfiguration configuration
) : IRequestHandler<AddVideoCommand, string?>
    {
        public async Task<string?> Handle(AddVideoCommand request, CancellationToken cancellationToken)
        {
            var url = GetUrl(request.LibraryId);
            var options = new RestClientOptions(url);
            var client = new RestClient(options);
            var httpRequest = new RestRequest("");
            var apiLibraryKey = configuration["BunnyCdn:ApiLibraryKey"]!;
            var accessKey = configuration["BunnyCdn:AccessKey"]!;
            httpRequest.AddHeader("accept", "application/json");
            httpRequest.AddHeader(accessKey, apiLibraryKey);
            httpRequest.AddBody(new
            {
                title = request.VideoName,
                collectionId = request.CollectionId
            });
            var response = await client.PostAsync(httpRequest, cancellationToken);
            var content = new JsonHelper(response);
            try
            {
                var videoId = content.GetValue<string>("guid");
                return videoId!;
            }
            catch (Exception)
            {
                return null;
            }
        }


        private static string GetUrl(string libraryId)
        {
            return $"https://video.bunnycdn.com/library/{libraryId}/videos";
        }
    }
}
