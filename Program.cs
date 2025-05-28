using csharp_demo_api.Infrastructure.Persistence;
using csharp_demo_api.Domain.Interfaces;
using csharp_demo_api.Infrastructure.Repositories;
using csharp_demo_api.Infrastructure.Persistence.Settings;
using csharp_demo_api.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<MongoDbSetting>(
    builder.Configuration.GetSection("MongoDbSettings")
);
builder.Services.AddSingleton<MongoDbContext>();

var useMongo = builder.Configuration.GetValue<bool>("UseMongo");

if (useMongo)
{
    builder.Services.AddScoped<ITaskRepository, TaskRepositoryMongo>();
}
else
{
    builder.Services.AddScoped<ITaskRepository, TaskRepository>();
}
builder.Services.AddScoped<TaskService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
