using Microsoft.EntityFrameworkCore;
using WebApiFormatter.Data;
using WebApiFormatter.Formatters;
using WebApiFormatter.Repositories.Abstracts;
using WebApiFormatter.Repositories.Concretes;
using WebApiFormatter.Services.Abstracts;
using WebApiFormatter.Services.Concretes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(opt =>
{
    opt.OutputFormatters.Insert(0, new VCardOutputFormatter()); 
    opt.InputFormatters.Insert(0, new VCardInputFormatter()); 
});  


builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();

var connection = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<UserDbContext>(opt =>
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
