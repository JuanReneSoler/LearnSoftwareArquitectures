using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db context
builder.Services.AddDbContext<SqlServerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

//Repositories
builder.Services.AddScoped<IGenericRepository<Tasks>, GenericRepository<Tasks>>();
builder.Services.AddScoped<IGenericRepository<Person>, GenericRepository<Person>>();
builder.Services.AddScoped<IGenericRepository<Group>, GenericRepository<Group>>();

//services
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IPersonService, PersonService>();

var app = builder.Build();

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
