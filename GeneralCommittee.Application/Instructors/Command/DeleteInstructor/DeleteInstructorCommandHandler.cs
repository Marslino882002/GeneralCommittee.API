using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Exceptions;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Command.DeleteInstructor
{
    public class DeleteInstructorCommandHandle(IInstructorRepository instructorRepository , 
        IConfiguration configuration,
     UserContext userContext,
     Logger<DeleteInstructorCommandHandle> logger,
    IAdminRepository adminRepository,
        Mediator mediator) : IRequestHandler<DeleteInstructorCommand>
    {
        public async Task Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {

            // Check if the current user is authenticated and has the 'Admin' role.
            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
            {
                throw new UnauthorizedAccessException();
            }
            var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);


            var Removed_Instructor = await instructorRepository.GetInstructorByIdOrName(request.InstructorId ,request.InstructorOfName);


            if (Removed_Instructor == null)
            {
                logger.LogWarning("Instructor with ID {id} not found.", request.InstructorId);
                throw new ResourceNotFound(nameof(Instructor), request.InstructorId.ToString());

            } // Delete the article
            await instructorRepository.DeleteInstructor(Removed_Instructor.InstructorId , Removed_Instructor.Name);
            logger.LogInformation("Instructor Deleted successfully with ID: {InstructorId}", request.InstructorId);
            //  return Unit.Value; // TODO :successful completion



        }
    }
}
