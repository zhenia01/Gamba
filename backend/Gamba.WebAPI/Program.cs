using Gamba.DataAccess.BuildingBlocks;
using Gamba.DataAccess.Users;
using Gamba.DataAccess.Users.Rules;
using Gamba.Infrastructure.Database;
using Gamba.WebAPI.Features;
using Gamba.WebAPI.SeedWork;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
builder.Services.AddFeatureModules();

builder.Services.AddDbContext<GambaContext>(
    opt => opt.UseNpgsql("Host=host.docker.internal:49153;Username=postgres;Password=postgrespw;Database=GambaTest"));

builder.Services.AddProblemDetails(opt =>
{
    opt.IncludeExceptionDetails = (_, _) => builder.Environment.IsDevelopment();
    
    opt.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserModule();

app.Run();