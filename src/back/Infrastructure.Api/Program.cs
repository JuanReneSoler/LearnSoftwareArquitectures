using Application.Services;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using EasyMapper;
using Application.Dtos;

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

//mapper
builder.Services.AddScoped(typeof(IMapper), (x =>
{
    var mapperConfig = new MapperConfiguration();
    mapperConfig.SetMapperProfile(profile =>
    {
        profile.CreateMap<Group, GroupDto>();
        profile.CreateMap<GroupDto, Group>();
        profile.CreateMap<Person, PersonDto>();
        profile.CreateMap<PersonDto, Person>();
        profile.CreateMap<Tasks, TaskDto>();
        profile.CreateMap<TaskDto, Tasks>();
    });
    return mapperConfig.CreateMapper();
}));

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
