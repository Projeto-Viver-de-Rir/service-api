using Institutional.Api.Boilerplate;
using Institutional.Api.Common;
using Institutional.Api.Configurations;
using Institutional.Api.Endpoints;
using Institutional.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.AddValidationSetup();

builder.Services.AddAuthorization();

// Adding cache operations
builder.Services.AddMemoryCache();

// Swagger
builder.Services.AddSwaggerSetup();

// Persistence
builder.Services.AddPersistenceSetup(builder.Configuration);

// Application layer setup
builder.Services.AddApplicationSetup();

// Add identity stuff
builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>(opt =>
    {
        opt.User.RequireUniqueEmail = true;
        opt.SignIn.RequireConfirmedAccount = true;
    })
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Request response compression
builder.Services.AddCompressionSetup();

// HttpContextAcessor
builder.Services.AddHttpContextAccessor();

// Mediator
builder.Services.AddMediatRSetup();

// Exception handler
builder.Services.AddExceptionHandler<ExceptionHandler>();

// Cookies to make it easier
builder.Services.ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.None;});

// Adding email provider
builder.Services.AddEmailSetup(builder.Configuration);

// Adding AWS provider
builder.Services.AddAWSSetup(builder.Configuration);

builder.Logging.ClearProviders();

// Add serilog
if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingSetup(builder.Configuration);
    
    // Add opentelemetry
    builder.AddOpenTemeletrySetup();
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .AllowAnyOrigin() 
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHsts();
app.UseCors("AllowAll");

// Swagger
app.UseSwaggerSetup();

// SPA
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapFallbackToFile("index.html");  

app.UseResponseCompression();
app.UseHttpsRedirection();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapAccountEndpoints();
app.MapVolunteerEndpoints();
app.MapDebtEndpoints();
app.MapEventEndpoints();
app.MapEventPresenceEndpoints();
app.MapScheduleEventEndpoints();
app.MapTeamEndpoints();
app.MapConfigEndpoints();
app.MapOperationEndpoints();
app.MapReportEndpoints();
app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapCustomIdentityApi<ApplicationUser>();

await app.RunAsync();