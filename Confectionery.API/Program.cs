using Confectionery.API.Extensions;
using MediatR;
using System.Reflection;
using Ñonfectionery.API.Application.Behaviors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DB
builder.Services.AddCustomDbContext(configuration);

// Configure Cors
builder.Services.AddCorsPolicies();

// Configure Mapster
builder.Services.AddMapster();

// Configure MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

// Configure Options
builder.Services.AddOptions(configuration);

// Configure Services
builder.Services.AddServices();

// Configure Repositories
builder.Services.AddRepositories();

var app = builder.Build();
app.UseCors("AllowedCorsOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
