using Hackathon.Api.Extention;
using Hackathon.Repository;
using Hackathon.Extension;
using Hackathon.Middleware;
using Hackathon.Service.BackgroundJobService;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using AuthsService = Hackathon.Service.Auths;
using MailServices = Hackathon.Service.MailServices;
using JwtServices = Hackathon.Service.JwtServices;
using EventsService = Hackathon.Service.Events;
using InvitationsService = Hackathon.Service.Invitations;
using RoundsService = Hackathon.Service.Rounds;
using TeamsService = Hackathon.Service.Teams;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;
using TracksService = Hackathon.Service.Tracks;
using CriticalsService = Hackathon.Service.Criticals;
using UserService = Hackathon.Service.Users;
using LeaderBoardsService = Hackathon.Service.LeaderBoards;
using SubmissionsService = Hackathon.Service.Submissions;
using MentorsService = Hackathon.Service.Mentors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<AuthsService.Request.RegisterRequest>();

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
            message: "INVALID_INPUT_DATA",
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

builder.Services.AddScoped<AuthsService.IService, AuthsService.Service>();
builder.Services.AddScoped<JwtServices.IService, JwtServices.Service>();
builder.Services.AddScoped<MailServices.IService, MailServices.Service>();
builder.Services.AddScoped<EventsService.IService, EventsService.Service>();
builder.Services.AddScoped<InvitationsService.IService, InvitationsService.Service>();
builder.Services.AddScoped<RoundsService.IService, RoundsService.Service>();
builder.Services.AddScoped<SubmissionsService.IService, SubmissionsService.Service>();
builder.Services.AddScoped<TeamsService.IService, TeamsService.Service>();
builder.Services.AddScoped<RegisterTeamsService.IService, RegisterTeamsService.Service>();
builder.Services.AddScoped<TracksService.IService, TracksService.Service>();
builder.Services.AddScoped<CriticalsService.IService, CriticalsService.Service>();
builder.Services.AddScoped<UserService.IService, UserService.Service>();
builder.Services.AddScoped<Hackathon.Service.Topics.IService, Hackathon.Service.Topics.Service>();
builder.Services.AddScoped<LeaderBoardsService.IService, LeaderBoardsService.Service>();
builder.Services.AddScoped<Hackathon.Service.AssignEvents.IService, Hackathon.Service.AssignEvents.Service>();
builder.Services.AddScoped<Hackathon.Service.AssignTracks.IService, Hackathon.Service.AssignTracks.Service>();
builder.Services.AddScoped<MentorsService.IService, MentorsService.Service>();


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