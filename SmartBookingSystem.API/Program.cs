
using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using SmartBookingSystem.API.Configurations;
using SmartBookingSystem.Application.Mapping;
using SmartBookingSystem.Application.Validators.Account;
using SmartBookingSystem.Domain.Interfaces;
using SmartBookingSystem.Infrastructure.Identity;
using SmartBookingSystem.Infrastructure.Repositories;



namespace SmartBookingSystem.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.ConfigureDbContext(builder.Configuration);

            builder.Services.AddIdentityConfiguration();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            builder.Services.AddValidatorsFromAssemblyContaining<RegisterValidator>();

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

            builder.Services.AddCorsConfiguration();
            builder.Services.AddDependencyInjection();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await IdentitySeeder.SeedRolesAndAdminAsync(services);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
