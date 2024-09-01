using Microsoft.EntityFrameworkCore;
using TaskList.Api.Extensions;
using Infrastructure.DependencyInyection;
using Application.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
const string allowOrigins = "AllowAnyOrigin";

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db context
builder.Services.AddDbContext(builder.Configuration.GetConnectionString("default"));

builder.Services.AddAuthorization();

//Unit of Work
builder.Services.AddUnitOfWork();

//mapper
builder.Services.AddMapper();

//inject services
builder.Services.AddServices();

//domain events;
builder.Services.AddDomainEvents();

//Uses Cases
builder.Services.AddUsesCases();

//configure cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(allowOrigins, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.AddJwt(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowOrigins);

app.UseMiddlewares();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
