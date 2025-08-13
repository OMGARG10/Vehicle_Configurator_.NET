using System.Text.Json.Serialization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Repositories;
using Vehicle_Configurator_Project.Services;
using Vehicle_Configurator_Project.Token;
using Microsoft.AspNetCore.Identity;
using Vehicle_Configurator_Project.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Vehicle_Configurator_Project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure DB context with MySQL
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")))
            .EnableSensitiveDataLogging() // logs param values (be careful in production)
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));


            // 1?? Add CORS for specific frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy.AllowAnyOrigin() // Your frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // JWT configuration
            var jwtSettings = builder.Configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorization();
            builder.Services.AddScoped<IJwtService, JwtService>();

            // Add services with JSON options to respect [JsonPropertyName] and serialize enums as strings
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Use attribute names as-is
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enums as strings
                });

            // Add services
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<IModelService, ModelService>();
            builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
            builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
            builder.Services.AddScoped<ISegmentRepository, SegmentRepository>();
            builder.Services.AddScoped<ISegmentService, SegmentService>();
            builder.Services.AddScoped<IComponentRepository, ComponentRepository>();
            builder.Services.AddScoped<IComponentService, ComponentService>();
            builder.Services.AddScoped<IAlternateComponentRepository, AlternateComponentRepository>();
            builder.Services.AddScoped<IAlternateComponentService, AlternateComponentService>();
            builder.Services.AddScoped<IVehicleDetailRepository, VehicleDetailRepository>();
            builder.Services.AddScoped<IVehicleDetailService, VehicleDetailService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            builder.Services.AddScoped<IInvoiceHeaderRepository, InvoiceHeaderRepository>();
            builder.Services.AddScoped<IInvoiceHeaderService, InvoiceHeaderService>();
            builder.Services.AddScoped<IInvoiceDetailRepository, InvoiceDetailRepository>();
            builder.Services.AddScoped<IInvoiceDetailService, InvoiceDetailService>();
            builder.Services.AddScoped<InvoiceService>();
            builder.Services.AddScoped<InvoiceDetailService>();
            builder.Services.AddScoped<AlternateComponentService>();



            var app = builder.Build();

            // Configure images folder
            app.UseStaticFiles();

            // Configure middleware pipeline BEFORE Run()
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // 2?? Enable CORS before Authorization
            app.UseCors("FrontendPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
