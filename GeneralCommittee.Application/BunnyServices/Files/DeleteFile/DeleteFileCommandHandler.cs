using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.Files.DeleteFile
{
    public class DeleteFileCommandHandler(
    IConfiguration configuration
    ) : IRequestHandler<DeleteFileCommand>
    {
        public async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {

            var storageZoneName = configuration["BunnyCdn:StorageZoneName"]!;
            var accessKey = configuration["BunnyCdn:StorageZoneAuthenticationKey"]!;
            var baseUrl = "storage.bunnycdn.com";

            var fileStorageUrl = $"https://{baseUrl}/{storageZoneName}/{request.Directory}/{request.FileName}";
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("AccessKey", accessKey);

            var response = await httpClient.DeleteAsync(fileStorageUrl, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Error uploading file: {response.ReasonPhrase}. Details: {errorContent}");
















        }
    }
}
