
using Microsoft.EntityFrameworkCore;
using TestAPI.TIT.Models;
using TestAPI.TIT.Repository;
using TestAPI.TIT.UnitWork;

namespace TestAPI.TIT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<ITI_newContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddCors(
    op =>
    {
        op.AddPolicy("AllowOrigin",
            p =>
            {
                p.AllowAnyOrigin();
                //p.WithOrigins()
                p.AllowAnyHeader();
                p.AllowAnyMethod();
                //p.WithMethods()
            });
    }
    );

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //  builder.Services.AddScoped(typeof(IGenaricRepository<Course>), typeof(GenaricRepository<Course>));
              builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));

            builder.Services.AddScoped<UnitOfWork>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors("AllowOrigin");
            app.MapControllers();

            app.Run();
        }
    }
}
