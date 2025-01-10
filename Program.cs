using ApiPrueba3V2._00.src.Data;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using ApiPrueba3V2._00.src.Interfaces;
using ApiPrueba3V2._00.src.Repository;
using ApiPrueba3V2._00.src.Interface;
using ApiPrueba3V2._00.src.Services;
using ApiPrueba3V2._00.src.Model;
using ApiPrueba3V2._00.src.Helpers;
using CloudinaryDotNet;



var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

string cloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME") ?? throw new ArgumentNullException("CLOUDINARY_CLOUD_NAME");
string apiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY") ?? throw new ArgumentNullException("CLOUDINARY_API_KEY");
string apiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET") ?? throw new ArgumentNullException("CLOUDINARY_API_SECRET");

    var cloudinaryAccount = new Account(
        cloudName,
        apiKey,
        apiSecret
    );
var cloudinary = new Cloudinary(cloudinaryAccount);
builder.Services.AddSingleton(cloudinary);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer",
        }
    );
    option.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                },
                new string[] { }
            },
        }
    );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") 
              .AllowAnyHeader()                     
              .AllowAnyMethod();                   
    });
});

string connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? "Data Source=database.db";
builder.Services.AddDbContext<ApplicationDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false; // Permitir contraseñas sin caracteres especiales
    options.Password.RequiredLength = 6; // Mínimo de 6 caracteres
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();


builder
    .Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme =
            opt.DefaultChallengeScheme =
            opt.DefaultForbidScheme =
            opt.DefaultScheme =
            opt.DefaultSignInScheme =
            opt.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidateAudience = true,
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    Environment.GetEnvironmentVariable("JWT_KEY")
                        ?? throw new ArgumentNullException("JWT: SigningKey")
                )
            ),
        };
    });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();
    await dbContext.Database.MigrateAsync();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseCors("AllowFrontend");

}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

