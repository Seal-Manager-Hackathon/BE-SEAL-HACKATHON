using Hackathon.Api.Extention;
using Hackathon.Repository;
using Hackathon.Extension;
using Hackathon.Middleware;
using Microsoft.EntityFrameworkCore;
using AuthService = Hackathon.Service.Auth;
using MailService = Hackathon.Service.MailService;
using JwtService = Hackathon.Service.JwtService;
using AssignEventsService = Hackathon.Service.AssignEvents;
using AssignTracksService = Hackathon.Service.AssignTracks;
using AwardsService = Hackathon.Service.Awards;
using CriteriaItemsService = Hackathon.Service.CriteriaItems;
using CriteriaTemplatesService = Hackathon.Service.CriteriaTemplates;
using EmailVerificationsService = Hackathon.Service.EmailVerifications;
using EventRolesService = Hackathon.Service.EventRoles;
using EventsService = Hackathon.Service.Events;
using InvitationsService = Hackathon.Service.Invitations;
using LeaderBoardDetailsService = Hackathon.Service.LeaderBoardDetails;
using LeaderBoardsService = Hackathon.Service.LeaderBoards;
using MentorNotificationsService = Hackathon.Service.MentorNotifications;
using NotificationsService = Hackathon.Service.Notifications;
using RefreshTokensService = Hackathon.Service.RefreshTokens;
using RegisterTeamsService = Hackathon.Service.RegisterTeams;
using ReportsService = Hackathon.Service.Reports;
using ResetPasswordsService = Hackathon.Service.ResetPasswords;
using RolesService = Hackathon.Service.Roles;
using RoundDetailsService = Hackathon.Service.RoundDetails;
using RoundsService = Hackathon.Service.Rounds;
using ScoreItemsService = Hackathon.Service.ScoreItems;
using ScoresService = Hackathon.Service.Scores;
using SubmissionsService = Hackathon.Service.Submissions;
using TeamDetailsService = Hackathon.Service.TeamDetails;
using TeamsService = Hackathon.Service.Teams;
using TopicsService = Hackathon.Service.Topics;
using TracksService = Hackathon.Service.Tracks;
using UserRolesService = Hackathon.Service.UserRoles;
using UsersService = Hackathon.Service.Users;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

builder.Services.AddControllers();
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

builder.Services.AddScoped<AuthService.IService, AuthService.Service>();
builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<MailService.IService, MailService.Service>();
builder.Services.AddScoped<AssignEventsService.IService, AssignEventsService.Service>();
builder.Services.AddScoped<AssignTracksService.IService, AssignTracksService.Service>();
builder.Services.AddScoped<AwardsService.IService, AwardsService.Service>();
builder.Services.AddScoped<CriteriaItemsService.IService, CriteriaItemsService.Service>();
builder.Services.AddScoped<CriteriaTemplatesService.IService, CriteriaTemplatesService.Service>();
builder.Services.AddScoped<EmailVerificationsService.IService, EmailVerificationsService.Service>();
builder.Services.AddScoped<EventRolesService.IService, EventRolesService.Service>();
builder.Services.AddScoped<EventsService.IService, EventsService.Service>();
builder.Services.AddScoped<InvitationsService.IService, InvitationsService.Service>();
builder.Services.AddScoped<LeaderBoardDetailsService.IService, LeaderBoardDetailsService.Service>();
builder.Services.AddScoped<LeaderBoardsService.IService, LeaderBoardsService.Service>();
builder.Services.AddScoped<MentorNotificationsService.IService, MentorNotificationsService.Service>();
builder.Services.AddScoped<NotificationsService.IService, NotificationsService.Service>();
builder.Services.AddScoped<RefreshTokensService.IService, RefreshTokensService.Service>();
builder.Services.AddScoped<RegisterTeamsService.IService, RegisterTeamsService.Service>();
builder.Services.AddScoped<ReportsService.IService, ReportsService.Service>();
builder.Services.AddScoped<ResetPasswordsService.IService, ResetPasswordsService.Service>();
builder.Services.AddScoped<RolesService.IService, RolesService.Service>();
builder.Services.AddScoped<RoundDetailsService.IService, RoundDetailsService.Service>();
builder.Services.AddScoped<RoundsService.IService, RoundsService.Service>();
builder.Services.AddScoped<ScoreItemsService.IService, ScoreItemsService.Service>();
builder.Services.AddScoped<ScoresService.IService, ScoresService.Service>();
builder.Services.AddScoped<SubmissionsService.IService, SubmissionsService.Service>();
builder.Services.AddScoped<TeamDetailsService.IService, TeamDetailsService.Service>();
builder.Services.AddScoped<TeamsService.IService, TeamsService.Service>();
builder.Services.AddScoped<TopicsService.IService, TopicsService.Service>();
builder.Services.AddScoped<TracksService.IService, TracksService.Service>();
builder.Services.AddScoped<UserRolesService.IService, UserRolesService.Service>();
builder.Services.AddScoped<UsersService.IService, UsersService.Service>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000")
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