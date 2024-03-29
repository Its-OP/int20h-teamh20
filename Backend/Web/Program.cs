using System.Text;
using System.Text.Json.Serialization;
using domain;
using domain.Interfaces;
using infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Helpers = backend.Controllers.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddResponseCaching();

var jwtIssuer = builder.Configuration.GetRequiredSection("Jwt:Issuer").Get<string>() ?? string.Empty;
var jwtKey = builder.Configuration.GetRequiredSection("Jwt:Key").Get<string>() ?? string.Empty;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

await using var scope = app.Services.CreateAsyncScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await context.Database.MigrateAsync();

if (!await context.Users.AnyAsync())
{
    var user = new User("default",
        "default",
        "default",
        "default",
        "admin@admin.com",
        await Helpers.GetPasswordHash("default", CancellationToken.None),
        Roles.Professor);

    await context.Users.AddAsync(user);
    await context.SaveChangesAsync();
}

app.UseResponseCaching();
app.UseCors(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();