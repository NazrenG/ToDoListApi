using Microsoft.EntityFrameworkCore;
using ToDoListApi.Data;
using ToDoListApi.Repositories.Abstracts;
using ToDoListApi.Repositories.Concretes;
using ToDoListApi.Services.Abstracts;
using ToDoListApi.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IToDoRepository,ToDoRepository>();
builder.Services.AddScoped<IToDoService,ToDoService>();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<ToDoListDbContext>(opt =>
{
    opt.UseSqlServer(connection);
});

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
