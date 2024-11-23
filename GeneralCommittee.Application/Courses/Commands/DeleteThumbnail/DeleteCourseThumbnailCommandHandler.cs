using GeneralCommittee.Application.BunnyServices.Files.DeleteFile;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Commands.DeleteThumbnail
{
    public class DeleteCourseThumbnailCommandHandler(
    ICourseRepository courseRepository,
    IMediator mediator
) : IRequestHandler<DeleteCourseThumbnailCommand>
    {


        public async Task Handle(DeleteCourseThumbnailCommand request, CancellationToken cancellationToken)
        {
            var course = await courseRepository.GetCourseByIdAsync(request.CourseId);
            if (course.ThumbnailName is null)
            {
                return;
            }
            var command = new DeleteFileCommand()
            {
                FileName = course.ThumbnailName,
                Directory = Global.CourseThumbnailDirectory
            };

            await mediator.Send(command, cancellationToken);
            course.ThumbnailUrl = null;
            await courseRepository.SaveChangesAsync();
        }









    }
}
