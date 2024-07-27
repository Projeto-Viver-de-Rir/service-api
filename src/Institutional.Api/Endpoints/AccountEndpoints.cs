using Ardalis.Result.AspNetCore;
using Institutional.Application.Common.Requests;
using Institutional.Application.Features.Accounts.Account;
using Institutional.Application.Features.Accounts.EnrollAccount;
using Institutional.Application.Features.Accounts.GetMyself;
using Institutional.Application.Features.Accounts.GetMyselfV2;
using Institutional.Application.Features.Accounts.Photo;
using Institutional.Application.Features.Accounts.Volunteer;
using Institutional.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Security.Claims;

namespace Institutional.Api.Endpoints;

public static class AccountEndpoints
{
    public static void MapAccountEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/account")
            .WithTags("account")
            .RequireAuthorization()
            .DisableAntiforgery();

        group.MapGet("/myself", async (IMediator mediator, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await mediator.Send(new GetMyselfRequest(Guid.Parse(loggedUserId)));
            return result.ToMinimalApiResult();
        });
        
        group.MapGet("/v2/myself", async (IMediator mediator, IHttpContextAccessor httpContextAccessor) =>
        {
            var loggedUserId = httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await mediator.Send(new GetMyselfV2Request(Guid.Parse(loggedUserId)));
            return result.ToMinimalApiResult();
        });
        
        group.MapPatch("/account", async (IMediator mediator, AccountRequest request, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            var user = await userManager.Users.SingleOrDefaultAsync(p => p.Id == audit.StartedBy);
            
            // Change PhoneNumber without verify token (for now) 
            var phoneToken = await userManager.GenerateChangePhoneNumberTokenAsync(user, request.Phone);
            await userManager.ChangePhoneNumberAsync(user, request.Phone, phoneToken);

            // Change Email without verify token (for now)
            var emailToken = await userManager.GenerateChangeEmailTokenAsync(user, request.Email);
            await userManager.ChangeEmailAsync(user, request.Email, emailToken);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapPost("/enroll", async (IMediator mediator, EnrollAccountRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapPatch("/photo", async (IMediator mediator, IFormFile file, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var request = new PhotoRequest { FileName = file.FileName, FileExtension =  Path.GetExtension(file.FileName)};
            request.Input = new MemoryStream();
            await file.CopyToAsync(request.Input);
            
            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
        
        group.MapPatch("/volunteer", async (IMediator mediator, VolunteerRequest request, IHttpContextAccessor httpContextAccessor) =>
        {
            var audit =
                new AuditData(httpContextAccessor?.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await mediator.Send(request with { AuditFields = audit });
            return result.ToMinimalApiResult();
        });
    }
}