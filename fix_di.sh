sed -i '/using TeamsService = Hackathon.Service.Teams;/a using RegisterTeamsService = Hackathon.Service.RegisterTeams;' D:/dotNet/Hackathon/Hackathon.Api/Program.cs
sed -i '/builder.Services.AddScoped<TeamsService.IService, TeamsService.Service>();/a builder.Services.AddScoped<RegisterTeamsService.IService, RegisterTeamsService.Service>();' D:/dotNet/Hackathon/Hackathon.Api/Program.cs
