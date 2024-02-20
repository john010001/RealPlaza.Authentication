using MediatR;
using RealPlaza.Authentication.Application.Features.Login.Commands.Registrar;
using RealPlaza.Authentication.Application.Wrappers;
using RealPlaza.Authentication.Application;
using RealPlaza.Authentication.Infrastructure;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using RealPlaza.Authentication.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
string policyCors = "PolicyCorsAllowAny";
// Add services to the container.



builder.Services.AddControllers();

//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddAplicationServices(configuration);
builder.Services.AddInfraestructureService(configuration);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(policyCors, policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});
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

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(policyCors);

app.UseAuthorization();


app.MapControllers();

app.Run();
