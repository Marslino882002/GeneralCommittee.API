using GeneralCommittee.Application.BunnyServices;
using GeneralCommittee.Application.BunnyServices.VideoContent.Video.CreateVideo;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Videos.Commands.CreateVideo
{
    public class CreateVideoCommandHandler(
    ICourseRepository courseRepository,
    IConfiguration configuration,
    IMediator mediator,
    UserContext userContext,
    IAdminRepository adminRepository
) : IRequestHandler<CreateVideoCommand, CreateVideoCommandResponse>
    {
       async Task<CreateVideoCommandResponse> IRequestHandler<CreateVideoCommand, CreateVideoCommandResponse>.Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {


            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
                throw new UnauthorizedAccessException();
            var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);
            var course = await courseRepository.GetCourseByIdAsync(request.CourseId);
            var libId = configuration["BunnyCdn:LibraryId"]!;
            var addVideoCommand = new AddVideoCommand
            {
                CollectionId = course.CollectionId,
                LibraryId = libId,
                VideoName = request.VideoName
            };
            var videoId = await mediator.Send(addVideoCommand, cancellationToken);
            //todo handle this error
            if (videoId == null)
                throw new Exception("UnExpected Error");
            var bunny = new BunnyClient(configuration);
            var ret = bunny.GenerateSignature(libId, course.CollectionId, videoId);
            await courseRepository.AddPendingUpload(new()
            {
                CreatedDate = DateTime.UtcNow,
                PendingVideoUploadId = videoId,
                CourseId = request.CourseId,
                Title = request.VideoName,
                Description = request.Description,
                Url = $"https://iframe.mediadelivery.net/play/{libId}/{videoId}",
                AdminId = admin.AdminId
            });
            return ret;








        }
    }
}
