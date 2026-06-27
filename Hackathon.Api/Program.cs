using Hackathon.Api.Extention;
using Hackathon.Api.Filters;
using Hackathon.Api.Localization;
using Hackathon.Repository;
using Hackathon.Extension;
using Hackathon.Middleware;
using Hackathon.Service.BackgroundJobService;
using Hackathon.Service.Localization;
using FluentValidation;
using FluentValidation.AspNetCore;
using Hackathon.Service.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Globalization;
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
using NotificationsService = Hackathon.Service.Notifications;
using JudgesService = Hackathon.Service.Judges;
using SystemsService = Hackathon.Service.Systems;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();
// Đăng ký service dịch message code -> text theo ngôn ngữ hiện tại.
builder.Services.AddScoped<IMessageLocalizer, MessageLocalizer>();

// Danh sách ngôn ngữ backend hỗ trợ ban đầu: English và Vietnamese.
var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("vi") };

// Khai báo thư mục chứa file .resx: Hackathon.Api/Resources.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
// Cấu hình cách ASP.NET chọn culture cho mỗi request.
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // Nếu request không gửi Accept-Language hoặc gửi ngôn ngữ chưa hỗ trợ thì dùng English.
    options.DefaultRequestCulture = new RequestCulture("en");
    // Culture dùng cho format số/ngày nếu sau này cần.
    options.SupportedCultures = supportedCultures;
    // UI culture dùng để chọn file SharedResource.{culture}.resx.
    options.SupportedUICultures = supportedCultures;
    // Đọc ngôn ngữ từ header Accept-Language của request.
    options.RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() };
});

builder.Services.AddControllers(options =>
{
    // Filter này dịch Message/Title trước khi trả JSON; MessageCode vẫn giữ nguyên cho FE xử lý logic.
    options.Filters.Add<LocalizationResponseFilter>();
});
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

        // Lấy localizer theo request hiện tại để validation error cũng đi theo Accept-Language.
        var localizer = context.HttpContext.RequestServices.GetRequiredService<IMessageLocalizer>();
        // Ưu tiên dịch lỗi validation đầu tiên; nếu không có thì dùng INVALID_INPUT_DATA.
        var firstError = errors?.FirstOrDefault().Value?.FirstOrDefault() ?? MessageKeys.InvalidInputData;

        var errorResponse = ApiResponseFactory.Error(
            // Title được dịch từ VALIDATION_FAILED_TITLE hoặc fallback theo HTTP 400.
            title: localizer.GetTitle(MessageKeys.ValidationFailed, StatusCodes.Status400BadRequest),
            status: StatusCodes.Status400BadRequest,
            // Message là lỗi cụ thể đã dịch, ví dụ FIRST_NAME_LENGTH_INVALID nếu có resource.
            message: localizer.Get(firstError),
            // MessageCode giữ nguyên để FE biết đây là lỗi validation.
            messageCode: MessageKeys.ValidationFailed,
            // errors giữ raw code theo từng field để FE/debug vẫn đọc được lỗi gốc.
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
builder.Services.AddScoped<NotificationsService.IService, NotificationsService.Service>();
builder.Services.AddScoped<JudgesService.IService, JudgesService.Service>();
builder.Services.AddScoped<Hackathon.Service.Lecturers.IService, Hackathon.Service.Lecturers.Service>();
builder.Services.AddScoped<Hackathon.Service.Staff.IService, Hackathon.Service.Staff.Service>();
builder.Services.AddScoped<SystemsService.IService, SystemsService.Service>();


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

// Kích hoạt localization sớm để middleware/validation/controller đều đọc được culture của request.
app.UseRequestLocalization();

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