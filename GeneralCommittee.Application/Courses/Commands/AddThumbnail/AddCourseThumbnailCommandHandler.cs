using GeneralCommittee.Application.BunnyServices.Files.UploadFile;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Commands.AddThumbnail
{
    public class AddCourseThumbnailCommandHandler(
    ILogger<AddCourseThumbnailCommandHandler> logger,
    ICourseRepository courseRepository,
    IMediator mediator
) : IRequestHandler<AddCourseThumbnailCommand, string>
    {
        public async Task<string> Handle(AddCourseThumbnailCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseByIdAsync(request.CourseId);
            var thumbnailName = request.CourseId + "-" + Guid.NewGuid() + Global.ThumbnailFileExtension;
            var uploadFileCommand = new UploadFileCommand
            {
                File = request.File,
                FileName = thumbnailName,
                Directory = Global.CourseThumbnailDirectory
            };
            var result = await mediator.Send(uploadFileCommand, cancellationToken);
            course.ThumbnailUrl = result;
            course.ThumbnailName = thumbnailName;
            await courseRepository.SaveChangesAsync();
            return result;
        }
    }
}
