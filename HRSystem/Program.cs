
using HRDomain.Repository;
using HRRepository;
using HRRepository.Data;
using HRSystem.Error_Handling;
using HRSystem.Helpers;
//using HRSystem.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using HRSystem.Helpers;
using Microsoft.EntityFrameworkCore;
using HRDomain.Entities;

namespace HRSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.


            #region Handling Validation Response
            builder.Services.Configure<ApiBehaviorOptions>(options => options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(m => m.Value.Errors.Count() > 0).SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToArray();

                var validationErrorResponse = new ValidationErrorResponse() 
                { 
                    Errors = errors 
                };
                return new BadRequestObjectResult(validationErrorResponse);
            });
            #endregion

            //Add HRContext Servise
            builder.Services.AddDbContext<HRContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<GenericRepository<Department>>();
            builder.Services.AddScoped<GenericRepository<Employee>>();


            //builder.Services.AddAutoMapper(typeof(MappingProfiles));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();
         
            #region Making update-database each time you run the project
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var context = services.GetRequiredService<HRContext>();
                await context.Database.MigrateAsync();

                //await HRContextSeed.SeedAsync(context,loggerFactory);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex,ex.Message);
            }
            #endregion

            // Configure the HTTP request pipeline.
            //use custom middleware
            //app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
