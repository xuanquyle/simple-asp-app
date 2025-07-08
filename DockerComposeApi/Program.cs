using DockerComposeApi;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_DEFAULTCONNECTION");
var mysqlConnectionString = Environment.GetEnvironmentVariable("ASPNETCORE_MYSQLCONNECTION");
var mysqlVersion = Environment.GetEnvironmentVariable("ASPNETCORE_MYSQLVERSION");


if (!string.IsNullOrEmpty(sqlConnectionString))
{
    builder.Services.AddDbContext<DockerCommandContext>(options => options.UseSqlServer(sqlConnectionString));
}
else if (!string.IsNullOrEmpty(mysqlConnectionString))
{
    builder.Services.AddDbContext<DockerCommandContext>(options => options.UseMySql(mysqlConnectionString, ServerVersion.Parse(mysqlVersion)));
}

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DockerCommandContext>();
    db.Database.Migrate(); // applies any pending migrations
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
