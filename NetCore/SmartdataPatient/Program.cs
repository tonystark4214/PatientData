using Microsoft.EntityFrameworkCore;
using SmartdataPatient.Controllers;
using SmartdataPatient.Models;
using SmartdataPatient.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(option => option.AddPolicy("cors", builder =>
    builder
    .AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod()

));
builder.Services.AddDbContext<sdirectdbContext>(p=>p.UseSqlServer("Server=192.168.0.240;Database=sdirectdb;UID=sdirectdb;PWD=sdirectdb;"));
builder.Services.AddScoped<IPatientData,PatientData>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();
app.UseCors("cors");
app.Run();
