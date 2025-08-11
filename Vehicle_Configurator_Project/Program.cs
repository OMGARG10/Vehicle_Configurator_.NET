using Microsoft.EntityFrameworkCore;
using Vehicle_Configurator_Project.IRepositories;
using Vehicle_Configurator_Project.IServices;
using Vehicle_Configurator_Project.Repositories;
using Vehicle_Configurator_Project.Services;

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
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
                ));

            // 1?? Add CORS for specific frontend
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("FrontendPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "https://localhost:5173") // Your frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            var app = builder.Build();

            // Configure middleware pipeline BEFORE Run()
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            // 2?? Enable CORS before Authorization
            app.UseCors("FrontendPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
