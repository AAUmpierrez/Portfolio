
using AplicationLogic.Common;
using BussinesLogic.RepositoryInterfaces;
using DataAccessLogic;
using DataAccessLogic.Repositories;
using Microsoft.EntityFrameworkCore;

namespace TicketFlowApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddMediatR(cfg =>
                                        cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly));

            string connectionString = builder.Configuration.GetConnectionString("ConnectionString");
            builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(connectionString));

            var app = builder.Build();

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
