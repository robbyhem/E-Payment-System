using AutoMapper;
using E_PaymentSystemAPI.Data;
using E_PaymentSystemAPI.Infrastructure;
using E_PaymentSystemAPI.IRepository;
using E_PaymentSystemAPI.IServices;
using E_PaymentSystemAPI.Repository;
using E_PaymentSystemAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Stripe;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationManager();

builder.Services
    .AddDbContext<PaymentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")))

    .AddScoped<IUser, UserRepository>()
    .AddScoped<IPayment, PaymentRepository>()
    .AddScoped<ITransaction, TransactionRepository>()
    .AddScoped<IRefund, RefundRepository>()
    .AddScoped<IAuthService, AuthService>();

// Add Stripe Infrastructure
builder.Services.AddStripeInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddEndpointsApiExplorer();

// Add Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add Authorization
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "E-PaymentSystemAPI", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme  // |c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme);|
    {
        Name = "Authorization",
        Description = "JWT Bearer token",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "bearer",
        BearerFormat = "JWT",
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirements = new OpenApiSecurityRequirement  // |c.AddSecurityRequirement(new OpenApiSecurityRequirement);|
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                //Scheme = "Oauth2",//
                //Name = "Bearer",//
                //In = ParameterLocation.Header,//
            },
            new List<string>()  // |Array.Empty<string>()|
        },
    };
    c.AddSecurityRequirement(securityRequirements);
});

// Authentication
var config = builder.Configuration.GetSection("Jwt");

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuer = false,
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ClockSkew = TimeSpan.FromSeconds(0), // |TimeSpan.Zero,|
    ValidIssuer = config["Jwt:Issuer"],
    ValidAudience = config["Jwt:Audience"],
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Key"]))
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = tokenValidationParameters;
    });

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
