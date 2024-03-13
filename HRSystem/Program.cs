
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
using AutoMapper;
using HRSystem.DTO;
using HRSystem.Middlewares;
using HRRepository.Identity;
using Microsoft.AspNetCore.Identity;
using HRDomain.Entities.Identity;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using HRDomain.Services;
using HRService;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace HRSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.


            #region Handling Validation Error

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .SelectMany(e => e.Value.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToArray();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });

            //builder.Services.Configure<ApiBehaviorOptions>(options =>

            //options.InvalidModelStateResponseFactory = (actionContext) =>
            //{
            //    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Count() > 0).SelectMany(m => m.Value.Errors).Select(m => m.ErrorMessage).ToList();

            //    var validationErrorResponse = new ValidationErrorResponse()
            //    {
            //        Errors = errors
            //    };
            //    return new BadRequestObjectResult(validationErrorResponse);
            //}) ;
            #endregion

            //Add HRContext Servise
            builder.Services.AddDbContext<HRContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("Default")); });

            #region Identity Services

            builder.Services.AddDbContext<AppIdentityDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")); });


            builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                //option.Password.RequireUppercase = false;
                //option.Password.RequireNonAlphanumeric = false;
                //option.User.RequireUniqueEmail = true;

            })
     .AddEntityFrameworkStores<AppIdentityDbContext>();

            #region Jwt Configurations
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });
            #endregion

            #endregion


            builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ITokenService, TokenService>();
                builder.Services.AddScoped<GenericRepository<Department>>();
                builder.Services.AddScoped<GenericRepository<Employee>>();
                builder.Services.AddScoped<GenericRepository<Vacation>>();
                builder.Services.AddScoped<GenericRepository<EmployeeAttendace>>();


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

                var identityContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityContext.Database.MigrateAsync();

                var manger= services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUsersAsync(manger);
            }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, ex.Message);
                }
                #endregion

                // Configure the HTTP request pipeline.

                //use custom middleware
                 app.UseMiddleware<ExceptionMiddleware>();
                if (app.Environment.IsDevelopment())
                {
                    //app.UseDeveloperExceptionPage();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }
                app.UseStaticFiles();


                app.UseHttpsRedirection();
                app.UseRouting();
               app.UseAuthentication();
                app.UseAuthorization();
                app.MapControllers();

                app.Run();
            }
    }
    } 
