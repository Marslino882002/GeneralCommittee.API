﻿using AutoMapper;
using GeneralCommittee.Application.BunnyServices.VideoContent.Collection.Add;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.Courses.Commands.Create
{
    public class CreateCourseCommandHandler(
    ILogger<CreateCourseCommandHandler> logger,
    IMapper mapper,
    ICourseRepository courseRepository,
    IMediator mediator
    ) : IRequestHandler<CreateCourseCommand, CreateCourseCommandResponse>
    {
        /// <summary>
        /// Processes the creation of a new course based on the given command.
        /// </summary>
        /// <param name="request">The command containing the details required to create a new course.</param>
        /// <param name="cancellationToken">Token to observe for cancellation requests.</param>
        /// <returns>A task that represents the result of the operation, containing the ID of the created course.</returns>
        /// 
        /// <remarks>
        /// The following describes the logic flow of the method:
        /// <list type="number">
        /// <item>
        /// <description>Log the start of the course creation process with the provided course name.</description>
        /// </item>
        /// <item>
        /// <description>Create a new collection:
        /// <list type="bullet">
        /// <item>Initialize an <c>AddCollectionCommand</c> with the collection name from the request.</item>
        /// <item>Send the collection command using the mediator to create the collection asynchronously.</item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>Map the request to a new <see cref="Course"/> object:
        /// <list type="bullet">
        /// <item>Use <c>mapper.Map&lt;Course&gt;(request)</c> to convert the command to a course entity.</item>
        /// <item>Set the <c>CollectionId</c> of the course to the ID returned from the collection creation.</item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>Insert the course into the database:
        /// <list type="bullet">
        /// <item>Call <c>courseRepository.CreateAsync(course)</c> to save the course asynchronously.</item>
        /// </list>
        /// </description>
        /// </item>
        /// <item>
        /// <description>Prepare the response:
        /// <list type="bullet">
        /// <item>Create a new <see cref="CreateCourseCommandResponse"/> with the ID of the newly created course.</item>
        /// </list>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        /// <exception cref="Exception">
        /// Thrown if there is an error during the creation of the course or collection.
        /// </exception>
        public async Task<CreateCourseCommandResponse> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            // Log the start of the course creation process
            logger.LogInformation("Creating new course: {CourseName}", request.Name);

            // Todo: upload image
            var collectionId = new AddCollectionCommand()
            {
                CollectionName = request.Name
            };

            // Log the creation of the collection
            logger.LogInformation("Creating collection for course: {CourseName}", request.Name);
            var result = await mediator.Send(collectionId, cancellationToken);

            // Log the created collection ID
            logger.LogInformation("Collection created with ID: {CollectionId}", result);

            var course = mapper.Map<Course>(request);
            course.CollectionId = result;

            // Log the insertion of the course into the repository
            logger.LogInformation("Inserting course into the repository: {CourseName}", request.Name);
            var id = await courseRepository.CreateAsync(course);

            // Log the successful creation of the course
            logger.LogInformation("Course created successfully with ID: {CourseId}", id);

            var ret = new CreateCourseCommandResponse
            {
                CourseId = id
            };
            return ret;
        }
    }
}
