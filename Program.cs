using apbd_cw12.Data;
using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Repositories;
using apbd_cw12.Services;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddDbContext<TripsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mssql.pjwstk.edu.pl")));

        builder.Services.AddScoped<IRepository<Client>, ClientRepository>();
        builder.Services.AddScoped<IRepository<ClientTrip>, ClientTripRepository>();
        builder.Services.AddScoped<IRepository<Trip>, TripRepository>();
        builder.Services.AddScoped<IRepository<Country>, CountryRepository>();

        builder.Services.AddScoped<IService<Client, ClientDto>, ClientService>();
        builder.Services.AddScoped<IService<ClientTrip, ClientTripDto>, ClientTripService>();
        builder.Services.AddScoped<IService<Trip, TripDto>, TripService>();
        builder.Services.AddScoped<ITripService, TripService>();
        builder.Services.AddScoped<IService<Country, CountryDto>, CountryService>();

        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen();

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
