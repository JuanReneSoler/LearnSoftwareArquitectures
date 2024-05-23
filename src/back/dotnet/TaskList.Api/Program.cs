using Microsoft.EntityFrameworkCore;
using TaskList.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
const string allowOrigins = "AllowAnyOrigin";

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//db context
builder.Services.InjectDbContext(builder.Configuration.GetConnectionString("default"));

//Repositories
builder.Services.InjectRepositories();

//mapper
builder.Services.InjectMapper();

//inject services
builder.Services.InjectServices();

//domain events;
builder.Services.InjectDomainEvents();

//Uses Cases
builder.Services.InjectUsesCases();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowOrigins);

app.InjectMiddlewares();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
