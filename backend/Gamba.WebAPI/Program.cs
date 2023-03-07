using Gamba.Application.Users.RegisterUser;
using Gamba.Infrastructure.Database;
using Gamba.WebAPI.Configuration;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Gamba.WebAPI.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtAuth(builder.Configuration);

builder.Services.AddSwagger();

builder.Services.AddMediatR(typeof(RegisterUserCommandHandler).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserCommand.Validator>(ServiceLifetime.Singleton);
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services.AddFeatureModules();

builder.Services.AddDbContext<GambaContext>(
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Gamba"), options =>
    {
        options.MigrationsAssembly(typeof(GambaContext).Assembly.GetName().Name);
    }));

builder.Services.MapProblemDetails();

builder.Services.AddControllers();

builder.Services.AddCors(options => options.AddDefaultPolicy(p =>
{
    p.WithOrigins(builder.Configuration["ClientUrl"]!)
        .AllowAnyHeader()
        .AllowAnyMethod();
}));

builder.WebHost.UseUrls("http://*:5050");

var app = builder.Build();

app.MigrateDatabase();

app.UseCors();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDev();
}

app.UseAuth();

app.UseHttpsRedirection();

var root = app.MapGroup("");
root.AddEndpointFilterFactory(ValidationFilter.ValidationFilterFactory);
root.MapFeatureModulesEndpoints();

app.Run();