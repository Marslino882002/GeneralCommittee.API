using GeneralCommittee.API.MiddleWares;
using GeneralCommittee.Application.Extensions;
using GeneralCommittee.Infrastructure.Persistence;
using GeneralCommittee.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;

namespace GeneralCommittee.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<GeneralCommitteeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.AddPresentation();
            //builder.Services.AddApplication(builder.Configuration);
            //builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<GlobalErrorHandling>();
            builder.Services.AddScoped<RequestTimeLogging>();
            builder.Services.AddControllers();

            // Add CORS services
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
  // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider.GetRequiredService<IAdminSeeder>();
            await serviceProvider.seed();
            // Use middlewares
            app.UseMiddleware<GlobalErrorHandling>();
            app.UseMiddleware<RequestTimeLogging>();
            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();
            app.UseRouting();

            // Use CORS middleware
            app.UseCors("AllowAll");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(); 
               
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
