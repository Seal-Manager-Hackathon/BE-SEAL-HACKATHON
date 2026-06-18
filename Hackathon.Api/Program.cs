using Hackathon.Api.Extention;
using Hackathon.Repository;
using Hackathon.Extension;
using Hackathon.Middleware;
using Hackathon.Service.BackgroundJobService;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using AuthService = Hackathon.Service.Auth;
using MailService = Hackathon.Service.MailService;
using JwtService = Hackathon.Service.JwtService;
using EventsService = Hackathon.Service.Events;
using InvitationsService = Hackathon.Service.Invitations;
using RoundsService = Hackathon.Service.Rounds;
using TeamsService = Hackathon.Service.Teams;
using TracksService = Hackathon.Service.Tracks;
using RegisterTeamService = Hackathon.Service.RegisterTeam;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value != null && e.Value.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value != null
                    ? kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    : Array.Empty<string>()
            );

        var errorResponse = ApiResponseFactory.Error(
            title: "Validation Failed",
            status: StatusCodes.Status400BadRequest,
            detail: "Dữ liệu đầu vào không hợp lệ.",
            messageCode: "VALIDATION_FAILED",
            errors: errors,
            traceId: context.HttpContext.TraceIdentifier
        );

        return new BadRequestObjectResult(errorResponse);
    };
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.ConfigureRateLimiter();
builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddQuartz(options =>
{
    var expirePendingEmailVerificationsJobKey = new JobKey(nameof(ExpirePendingEmailVerificationsJob));

    options.AddJob<ExpirePendingEmailVerificationsJob>(job =>
        job.WithIdentity(expirePendingEmailVerificationsJobKey));

    options.AddTrigger(trigger => trigger
        .ForJob(expirePendingEmailVerificationsJobKey)
        .WithIdentity($"{nameof(ExpirePendingEmailVerificationsJob)}-trigger")
        .WithSimpleSchedule(schedule => schedule
            .WithIntervalInMinutes(2)
            .RepeatForever()));
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
});

builder.Services.AddScoped<AuthService.IService, AuthService.Service>();
builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<MailService.IService, MailService.Service>();
builder.Services.AddScoped<EventsService.IService, EventsService.Service>();
builder.Services.AddScoped<InvitationsService.IService, InvitationsService.Service>();
builder.Services.AddScoped<RoundsService.IService, RoundsService.Service>();
builder.Services.AddScoped<TeamsService.IService, TeamsService.Service>();
builder.Services.AddScoped<TracksService.IService, TracksService.Service>();
builder.Services.AddScoped<RegisterTeamService.IService, RegisterTeamService.Service>();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .SetIsOriginAllowed(_ => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
var app = builder.Build();



app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseSwaggerAPI();

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();