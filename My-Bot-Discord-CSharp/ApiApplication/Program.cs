using ApiApplication;
using ApiApplication.Authentification.Interface;
using ApiApplication.Authentification.Manager;
using ApiApplication.Repository;
using ApiApplication.Repository.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceClassLibrary.Interfaces;
using ServiceClassLibrary.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var AppSetting = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json")
		.Build();

var services = builder.Services;
services.AddCors();
services.AddControllers();
services.AddDbContext<ApplicationDbContext>();


services.AddCors();
services.AddControllers();



services.AddTransient<JwtRepository>();



services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
	var Key = Encoding.UTF8.GetBytes(AppSetting["JWT:Key"]);
	o.SaveToken = true;
	o.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = AppSetting["JWT:Issuer"],
		ValidAudience = AppSetting["JWT:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Key)
	};
});




services.AddScoped(typeof(IJWTManagerRepository), typeof(JWTManagerRepository));

services.AddScoped(typeof(IGenericRepository<>), typeof(APIGenericRepository<>));
services.AddScoped(typeof(ILoggerProject), typeof(LoggerProject));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(APIGenericRepository<>));
builder.Services.AddScoped(typeof(ILoggerProject), typeof(LoggerProject));
builder.Services.AddScoped<Microsoft.EntityFrameworkCore.DbContext, ApplicationDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
	app.UseCors(x => x
			   .AllowAnyMethod()
			   .AllowAnyHeader()
			   .SetIsOriginAllowed(origin => true) // allow any origin
			   .AllowCredentials()); // allow credentials
}

app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(x => x.MapControllers());

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


