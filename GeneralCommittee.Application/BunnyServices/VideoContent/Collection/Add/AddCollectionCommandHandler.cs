﻿using GeneralCommittee.Application.Common;
using MediatR;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.BunnyServices.VideoContent.Collection.Add
{
    public class AddCollectionCommandHandler(
    IConfiguration configuration
) : IRequestHandler<AddCollectionCommand, string>
    {
        public async Task<string> Handle(AddCollectionCommand request, CancellationToken cancellationToken)
        {

            var libraryId = configuration["BunnyCdn:LibraryId"]!;
            var url = GetUrl(libraryId);
            var options = new RestClientOptions(url);
            var client = new RestClient(options);
            var httpRequest = new RestRequest("");
            var apiLibraryKey = configuration["BunnyCdn:ApiLibraryKey"]!;
            var accessKey = configuration["BunnyCdn:AccessKey"]!;
            httpRequest.AddHeader("accept", "application/json");
            httpRequest.AddHeader(accessKey, apiLibraryKey);
            httpRequest.AddBody(new { name = request.CollectionName });
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

        private string GetUrl(string requestLibraryId)
        {
            return $"https://video.bunnycdn.com/library/{requestLibraryId}/collections";
        }
    }
    }
/*
 * using RestSharp;


   var options = new RestClientOptions("https://video.bunnycdn.com/library/317728/collections");
   var client = new RestClient(options);
   var request = new RestRequest("");
   request.AddHeader("accept", "application/json");
   request.AddHeader("AccessKey", "709aef68-e4a2-4587-b27d90b0fc7b-7ac0-4fc3");
   request.AddJsonBody("{\"name\":\"colcol\"}", false);
   var response = await client.PostAsync(request);

   Console.WriteLine("{0}", response.Content);

 */
