using AutoMapper;
using GeneralCommittee.Application.Articles.Commands.AddCArticle;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Instructors.Command.AddNewInstructor.AddInstructorCommand
{
    public class AddInstructorCommandHandler
        (IInstructorRepository instructorRepository,
    IConfiguration configuration,
    IMapper mapper, UserContext userContext,
    IAdminRepository adminRepository,
    ILogger<AddInstructorCommandHandler> logger,
    IMediator mediator) : IRequestHandler<AddInstructorCommand, AddInstructorCommandResponse>
    {
        public async Task<AddInstructorCommandResponse> Handle(AddInstructorCommand request, CancellationToken cancellationToken)
        {


            var currentUser = userContext.GetCurrentUser();
            if (currentUser == null || !currentUser.HasRole(UserRoles.Admin))
                logger.LogError("Unauthorized access attempt by user.");
            throw new UnauthorizedAccessException();
            var admin = await adminRepository.GetAdminByIdentityAsync(currentUser.Id);



            // Validation: Check if the required fields are not null or empty
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.About))
       { logger.LogError("One or more required fields are empty."); 
                throw new ArgumentException("All fields must be provided."); }




            //TODO: Create the Article entity from the command
            var article = new Instructor
            {
                Name = request.Name,
                About = request.About,
                ImageUrl = request.ImageUrl.ToString(),
                AddedBy = request.AddedBy,
                
            };


            var result = await mediator.Send(article, cancellationToken);

            if (string.IsNullOrEmpty(request.Name) ||
    string.IsNullOrEmpty(request.About))
            {
                logger.LogError("One or more required fields are empty.");
                throw new ArgumentException("All fields must be provided.");
            }



            //TODO: Save The article In DB using the repository
            var id = await instructorRepository.AddInstructor(article);
            logger.LogInformation("Instructor created successfully with This Information: {ArticleId}", result);

            var New_Instructor = mapper.Map<Instructor>(request);
            var ret = new AddInstructorCommandResponse
            {
                InstructorID = id
            };
            return ret;








        }
    }
}
