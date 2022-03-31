using ApiApplication;
using ApiApplication;
using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var services = builder.Services;
services.AddCors();
services.AddControllers();
services.AddDbContext<ApplicationDbContext>();

services.AddCors();
services.AddControllers();


services.AddScoped(typeof(IGenericRepository<>), typeof(APIGenericRepository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(APIGenericRepository<>));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext, ApplicationDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors("AllowAnyOrigin");
}

app.UseRouting();



app.UseEndpoints(x => x.MapControllers());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


