﻿using AutoMapper;
using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.SystemUsers.Commands.Register;
using GeneralCommittee.Domain.Constants;
using GeneralCommittee.Domain.Entities;
using GeneralCommittee.Domain.Exceptions;
using GeneralCommittee.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralCommittee.Application.AdminUsers.Commands.Register
{/// <summary>
/// Handles the registration of a new admin user.
/// </summary>
/// <param name="request">The command containing registration details for the admin user.</param>
/// <param name="cancellationToken">Token to observe for cancellation requests.</param>
/// <returns>A task that represents the completion of the operation.</returns>
/// 
/// <remarks>
/// The following describes the logic flow of the method:
/// <list type="number">
/// <item>
/// <description>Log the start of the registration process for the email provided.</description>
/// </item>
/// <item>
/// <description>Check for a valid tenant: 
/// <list type="bullet">
/// <item>Ensure the <c>Tenant</c> field is not null or empty.</item>
/// <item>Verify the tenant matches the expected program name. If not, log a warning and throw an exception.</item>
/// </list>
/// </description>
/// </item>
/// <item>
/// <description>Check for the existence of the pending admin:
/// <list type="bullet">
/// <item>Call <c>IsPendingExistAsync</c> to confirm the email is in the pending list.</item>
/// <item>If not pending, log a warning and throw an exception.</item>
/// </list>
/// </description>
/// </item>
/// <item>
/// <description>Create a new user and admin:
/// <list type="bullet">
/// <item>Map the request to a new <c>User</c> object.</item>
/// <item>Create a new <c>Admin</c> object with the mapped user and provided names.</item>
/// <item>Add the Admin role to the user.</item>
/// </list>
/// </description>
/// </item>
/// <item>
/// <description>Insert the user into the database:
/// <list type="bullet">
/// <item>Log the insertion attempt and call <c>RegisterUser</c>.</item>
/// <item>If the user already exists, log a warning and throw an exception.</item>
/// </list>
/// </description>
/// </item>
/// <item>
/// <description>Send confirmation email:
/// <list type="bullet">
/// <item>After successful registration, call <c>SendConfirmation</c> to send a confirmation email.</item>
/// </list>
/// </description>
/// </item>
/// <item>
/// <description>Log success:
/// <list type="bullet">
/// <item>Log a success message after registration.</item>
/// </list>
/// </description>
/// </item>
/// </list>
/// </remarks>
/// <exception cref="ForBidenException">
/// Thrown if the tenant is invalid or the user is not allowed to register with the provided email.
/// </exception>
/// <exception cref="AlreadyExist">Thrown if the user already exists.</exception>
    public class RegisterAdminCommandHandler(
    ILogger<RegisterCommandHandler> logger,
    IUserRepository userRepository,
    IAdminRepository adminRepository,
    IEmailSender emailSender,
    UserManager<User> userManager,
    IMapper mapper
) : IRequestHandler<RegisterAdminCommand>
    {
        public async Task Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {


            logger.LogInformation("Register systemUser with Email : {@user}", request.Email);

            if (string.IsNullOrEmpty(request.Tenant))
            {
                logger.LogInformation("Invalid Tenant");
                throw new Exception("Invalid Tenant");
            }

            if (request.Tenant != Global.ProgramName)
                throw new ForBidenException("Not allowed");

            if (!await adminRepository.IsPendingExistAsync(request.Email))
                throw new ForBidenException($"{request.UserName} Can't register with email {request.Email}");

            User user = mapper.Map<User>(request);
            Admin admin = new()
            {
                User = user,
                FName = request.FirstName,
                LName = request.LastName
            };
            user.Roles = Roles.AddRole(user.Roles, UserRoles.Admin);
            logger.LogInformation("Inserting {@user} to the DB", request.UserName);
            var isInserted = await adminRepository.RegisterUser(user, request.Password, admin);
            if (!isInserted)
            {
                //todo return error in the admin repo
                logger.LogInformation("User {@user} already exists", request.UserName);
                throw new AlreadyExist($"{request.UserName} already exists");
            }

            logger.LogError("User {@user} Registered successfully", request.UserName);
            await adminRepository.DeletePendingAsync([request.Email]);
            await SendConfirmation(user);
            logger.LogInformation("User {@user} Registered successfully", request.UserName);
        }

        private async Task SendConfirmation(User user)
        {
            logger.LogInformation("Sending confirmation email to {@user}", user.Email);
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            await emailSender.SendEmailAsync(user.Email!, "Token to confirm your Email ", token);
        }



















    }
    }

