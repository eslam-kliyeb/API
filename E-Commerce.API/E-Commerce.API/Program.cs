using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.API.Middlewares;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Service;
using E_Commerce.Repository.Data;
using E_Commerce.Repository.Repositories;
using E_Commerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Reflection;
namespace E_Commerce.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(sql => {
                sql.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
            });
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.AddScoped<ICashService, CashService>();
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            //builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddSingleton<IConnectionMultiplexer>(options => 
            {
                var config = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("RedisConnection"));
                return ConnectionMultiplexer.Connect(config);
            });
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //===================================BadRequest====================================
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //Model State =>  Dic[KeyValuePair]
                    // Key=>Name of Param
                    // Value => error
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(p => p.ErrorMessage)
                                                         .ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            //====================================================================
            //====================================================================
            var app = builder.Build();
            await InitializeDbAsync(app);
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //===================My MiddleWare=========================
                app.UseMiddleware<ExceptionMiddleWare>();
                //=============================================
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            //app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<DataContext>();
                    //Create DB if it does not exist
                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                    {
                        await context.Database.MigrateAsync();
                    }
                    //Apply Seeding
                    await DataContextSeed.SeedDataAsync(context);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
