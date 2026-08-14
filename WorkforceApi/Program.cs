using WorkforceApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// service to register controller support
builder.Services.AddControllers();

// service to register dbcontext with the sqlserver
builder.Services.AddDbContext<WorkforceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkforceDb")));

// service to allow requests from frontend with temporary localhost ip
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// service to register authentication validation with tokens
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

var app = builder.Build();

app.UseCors("AllowFrontend"); 

app.UseHttpsRedirection();

// authenticating before authorizing
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();