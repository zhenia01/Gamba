using Gamba.Application.Users.RegisterUser;
using Gamba.DataAccess.BuildingBlocks;
using Gamba.Infrastructure.Database;
using Gamba.WebAPI.Configuration;
using Gamba.WebAPI.SeedWork;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddSwagger();

builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);

builder.Services.AddFeatureModules();

builder.Services.AddDbContext<GambaContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Gamba"), options =>
    {
        options.MigrationsAssembly(typeof(GambaContext).Assembly.GetName().Name);
    }));

builder.Services.AddProblemDetails(opt =>
{
    opt.IncludeExceptionDetails = (_, _) => builder.Environment.IsDevelopment();
    
    opt.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
});

builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddDefaultPolicy(p =>
{
    p.WithOrigins(builder.Configuration["ClientUrl"]!)
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

builder.WebHost.UseUrls("http://*:5050");

var app = builder.Build();

app.UseDatabaseMigrate();

app.UseCors();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDev();
}

app.UseAuth();

app.UseHttpsRedirection();

app.MapFeatureModulesEndpoints();

app.Run();