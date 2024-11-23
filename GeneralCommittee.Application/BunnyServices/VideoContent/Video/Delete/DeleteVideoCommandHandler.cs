using MediatR;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Video.Delete
{
    public class DeleteVideoCommandHandler(
    IConfiguration configuration
) : IRequestHandler<DeleteVideoCommand, bool>
    {
        public async Task<bool> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var libraryId = configuration["BunnyCdn:LibraryId"]!;
            var url = GetUrl(libraryId, request.VideoId);
            var options = new RestClientOptions(url);
            var client = new RestClient(options);
            var httpRequest = new RestRequest("");
            var apiLibraryKey = configuration["BunnyCdn:ApiLibraryKey"]!;
            var accessKey = configuration["BunnyCdn:AccessKey"]!;
            httpRequest.AddHeader("accept", "application/json");
            httpRequest.AddHeader(accessKey, apiLibraryKey);
            var response = await client.DeleteAsync(httpRequest, cancellationToken);
            return response.IsSuccessful;




        }
        private static string GetUrl(string libraryId, string videoId)
        {
            return $"https://video.bunnycdn.com/library/{libraryId}/videos/{videoId}";
        }
    }
}
