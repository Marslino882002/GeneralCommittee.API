using GeneralCommittee.API.MiddleWares;
using GeneralCommittee.Application.Extensions;
using GeneralCommittee.Infrastructure.Persistence;
using GeneralCommittee.Infrastructure.Seeders;
using MentalHealthcare.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using GeneralCommittee.Domain.Repositories;
using GeneralCommittee.Infrastructure.Repositories;
using GeneralCommittee.Application.SystemUsers;
using GeneralCommittee.Application.Videos.Commands.CreateVideo;
using GeneralCommittee.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR;
using GeneralCommittee.Domain.Entities;
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


            builder.Services.AddIdentity<User, IdentityRole>()
           .AddEntityFrameworkStores<GeneralCommitteeDbContext>()
           .AddDefaultTokenProviders();





            builder.AddPresentation();
          //  builder.Services.AddApplication(builder.Configuration);
        //    builder.Services.AddInfrastructure(builder.Configuration);


            builder.Services.AddScoped<GlobalErrorHandling>();
            builder.Services.AddScoped<RequestTimeLogging>();
            builder.Services.AddControllers();

           builder.Services.AddScoped<IAdminSeeder, AdminSeeder>();
            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
           builder.Services.AddScoped<IAdminRepository, AdminRepository>();
          builder.Services.AddTransient<IUserRepository, UserRepository>();
           builder.Services.AddTransient<IVideoStreamService , VideoStreamService>();
            builder.Services.AddScoped<UserContext>();
            // builder.Services.AddTransient<CreateVideoCommand, CreateVideoCommandHandler>();
          builder.Services.AddScoped<ISearchServiceRepository<object>, SearchServiceRepository>();
            builder.Services.AddSignalR();




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
            app.MapHub<Hub>("/notificationHub");

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















/*InvalidOperationException:
 * Error while validating the service descriptor 
 * 'ServiceType: MediatR.IRequestHandle
 * r`2[GeneralCommittee.Application.Videos.Commands.CreateVideo.CreateVideoCommand
 * ,GeneralCommittee.Application.Videos.Commands.CreateVideo.CreateVideoCommandResponse]
 * Lifetime: Transient ImplementationType: GeneralCommittee.Application.Videos.Commands.CreateVideo.
 * CreateVideoCommandHandler': Unable to 
 * resolve service for type 'GeneralCommittee.Application.
 * SystemUsers.UserContext' while attempting to activate 
 * 'GeneralCommittee.Application.Videos.Commands.CreateVideo.CreateVideoCommandHandler'.*/

/*InvalidOperationException: Unable to resolve service for 
 * type 'GeneralCommittee.Application.SystemUsers.
 * UserContext' while attempting to activate 'Ge
 * dneralCommittee.Application.Videos.Commands.CreateVideo.CreateVideoCommandHandler'.*/