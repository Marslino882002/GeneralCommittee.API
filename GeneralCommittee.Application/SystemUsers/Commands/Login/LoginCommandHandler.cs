using GeneralCommittee.Application.Common;
using GeneralCommittee.Application.SystemUsers.Commands.ConfirmEmail;
using GeneralCommittee.Application.Utitlites.Jwt;
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

namespace GeneralCommittee.Application.SystemUsers.Commands.Login
{
    public class LoginCommandHandler(
    ILogger<ConfirmEmailCommandHandler> logger,
    UserManager<User> userManager,
    IEmailSender emailSender,
    IJwtToken jwtToken,
    IUserRepository userRepository
) : IRequestHandler<LoginCommand, OperationResult<LoginDto>>
    {
        public async Task<OperationResult<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {


            User? user;

            // Validate tenant information
            if (string.IsNullOrEmpty(request.Tenant))
            {
                logger.LogWarning("Invalid tenant information provided for login.");
                return OperationResult<LoginDto>.Failure("Bad Request", StateCode.BadRequest);
            }

            logger.LogInformation("Login request for user: {UserIdentifier}", request.UserIdentifier);

            // Retrieve user based on identifier
            if (request.UserIdentifier.IsValidEmail())
            {
                user = await userRepository.GetUserByEmailAsync(request.UserIdentifier, request.Tenant!);
            }
            else if (request.UserIdentifier.IsValidPhoneNumber())
            {
                user = await userRepository.GetUserByPhoneNumberAsync(request.UserIdentifier, request.Tenant!);
            }
            else
            {
                user = await userRepository.GetUserByUserNameAsync(request.UserIdentifier, request.Tenant!);
            }

            // Check if the user exists
            if (user == null)
            {
                logger.LogError("User with identifier {UserIdentifier} not found.", request.UserIdentifier);
                throw new ResourceNotFound("User", request.UserIdentifier);
            }

            // Verify tenant association
            if (user.Tenant != request.Tenant)
            {
                logger.LogWarning("User {UserIdentifier} belongs to tenant {Tenant1} but attempted to log in with tenant {Tenant2}.",
                    request.UserIdentifier, user.Tenant, request.Tenant);
                return OperationResult<LoginDto>.Failure("Bad Request", StateCode.BadRequest);
            }

            // Check if email is confirmed
            if (!await userManager.IsEmailConfirmedAsync(user))
            {
                logger.LogWarning("User {Email} has not confirmed their email.", user.Email);
                return OperationResult<LoginDto>.Failure("Bad Request", StateCode.BadRequest);
            }

            // Validate password
            if (!await userManager.CheckPasswordAsync(user, request.Password))
            {
                logger.LogWarning("Invalid password attempt for user {UserIdentifier}.", request.UserIdentifier);
                return OperationResult<LoginDto>.Failure("Unauthorized", StateCode.Unauthorized);
            }

            // Check for two-factor authentication
            if (await userManager.GetTwoFactorEnabledAsync(user))
            {
                logger.LogInformation("2FA is enabled for user {UserIdentifier}. Sending 2FA code.",
                    request.UserIdentifier);

                var otp = await userManager.GenerateTwoFactorTokenAsync(user, "Email");
                await emailSender.SendEmailAsync(user.Email!, "Your 2FA Code", $"Your code is {otp}");
                return OperationResult<LoginDto>.SuccessResult(new LoginDto(), "2FA code sent");
                // TODO: Send OTP via phone
            }

            logger.LogInformation("Returning token for user {UserIdentifier}.", user.UserName);
            var tokens = await jwtToken.GetTokens(user);
            return OperationResult<LoginDto>.SuccessResult(new LoginDto()
            {
                Name = user.UserName!,
                Token = tokens.Item1,
                RefreshToken = tokens.Item2
            }, "Success");





        }
    }
}
