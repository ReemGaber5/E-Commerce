
using Abstraction;
using Domain.Interfaces;
using E_Commerce.CustomMiddleWare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.MappingProfiles;
using Shared.ErrorModels;
using StackExchange.Redis;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDbInitializer,DbInitializer>();
            builder.Services.AddScoped<IUOW, UOW>();
            builder.Services.AddScoped<IServiceManger,ServiceManger>();

            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly,typeof(ProductResolver).Assembly);

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionstring);

            });

            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var Errors = context.ModelState.Where(m => m.Value.Errors.Any()).
                    Select(m => new ValidationError()
                    {
                        Field=m.Key,
                        Errors=m.Value.Errors.Select(E=> E.ErrorMessage)

                    });
                    var Response = new ValidationErrorToReturn()
                    {
                        Errors = Errors
                    };
                    return new BadRequestObjectResult(Response);
                };
            });

            builder.Services.AddScoped<IBasketRepository,BasketRepository>();   

            builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
            {
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnectionString"));

            });
            var app = builder.Build();

           using var scope= app.Services.CreateScope();
            var dbinitializer= scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbinitializer.InitializeAsync();


            app.UseMiddleware<CustomExceptionMiddleWare>();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
