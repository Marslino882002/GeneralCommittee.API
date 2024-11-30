﻿using GeneralCommittee.Application.SystemUsers.Commands.AddRoles;
using GeneralCommittee.Application.SystemUsers.Commands.ChangePassword;
using GeneralCommittee.Application.SystemUsers.Commands.ConfirmEmail;
using GeneralCommittee.Application.SystemUsers.Commands.ForgetPassword;
using GeneralCommittee.Application.SystemUsers.Commands.Login;
using GeneralCommittee.Application.SystemUsers.Commands.Refresh;
using GeneralCommittee.Application.SystemUsers.Commands.Register;
using GeneralCommittee.Application.SystemUsers.Commands.RemoveRoles;
using GeneralCommittee.Application.SystemUsers.Commands.ResendEmailConfirmtion;
using GeneralCommittee.Application.SystemUsers.Commands.ResetPassword;
using GeneralCommittee.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GeneralCommittee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsUsersIdentityController(
    IMediator mediator
) : ControllerBase
    {

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            command.Tenant = Global.ApplicationTenant;
            var commandResult = await mediator.Send(command);
            if (commandResult.StatusCode == StateCode.Created)
                return Ok(commandResult);
            return BadRequest(commandResult);
        }

        [HttpPost(nameof(ConfirmEmail))]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            command.Tenant = Global.ApplicationTenant;

            var commandResult = await mediator.Send(command);
            if (commandResult.StatusCode == StateCode.Ok)
            {
                return Ok(commandResult);
            }

            return BadRequest(commandResult);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            command.Tenant = Global.ApplicationTenant;

            var commandResult = await mediator.Send(command);
            return Ok(commandResult);
        }

        [HttpPost(nameof(Refresh))]
        public async Task<IActionResult> Refresh(RefreshCommand command)
        {
            var commandResult = await mediator.Send(command);
            return commandResult.StatusCode switch
            {
                StateCode.Ok => Ok(commandResult),
                StateCode.Unauthorized => Unauthorized(commandResult),
                _ => BadRequest(commandResult)
            };
        }

        [HttpPost(nameof(ResendConfirmationEmail))]
        public async Task<IActionResult> ResendConfirmationEmail(
            ResendEmailConfirmationCommand command)
        {
            command.Tenant = Global.ApplicationTenant;

            var commandResult = await mediator.Send(command);
            return commandResult.StatusCode switch
            {
                StateCode.Ok => Ok(commandResult),
                StateCode.NotFound => NotFound(commandResult),
                _ => BadRequest(commandResult)
            };
        }

        [HttpPost(nameof(ForgetPassword))]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand command)
        {
            command.Tenant = Global.ApplicationTenant;

            var commandResult = await mediator.Send(command);
            return commandResult.StatusCode switch
            {
                StateCode.Ok => Ok(commandResult),
                StateCode.NotFound => NotFound(commandResult),
                _ => BadRequest(commandResult)
            };
        }

        [HttpPost(nameof(ResetPassword))]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            command.Tenant = Global.ApplicationTenant;

            var commandResult = await mediator.Send(command);
            return Ok(commandResult);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(nameof(ChangePassword))]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var commandResult = await mediator.Send(command);

            return commandResult.StatusCode switch
            {
                StateCode.Ok => Ok(commandResult),
                StateCode.NotFound => NotFound(commandResult),
                _ => BadRequest(commandResult)
            };
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost(nameof(Roles))]
        public async Task<IActionResult> Roles(AddRolesCommand command)
        {
            var commandResult = await mediator.Send(command);
            return Ok(commandResult);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("Roles")]
        public async Task<IActionResult> RemoveRoles(RemoveRolesCommand command)
        {
            var commandResult = await mediator.Send(command);
            return Ok(commandResult);
        }
























    }
}