using Microsoft.AspNetCore.Identity;
using system_przesylania_projektow.Infrastructure;
using system_przesylania_projektow.Application;
using system_przesylania_projektow.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontEndClient");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapIdentityApi<IdentityUser>();

app.Run();

public partial class Program { }
