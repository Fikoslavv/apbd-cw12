using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Repositories;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12.Services;

public class TripService : ITripService
{
    protected readonly IRepository<Trip> tripRepository;
    protected readonly IRepository<Client> clientRepository;
    protected readonly IRepository<Country> countryRepository;

    public TripService(IRepository<Trip> tripRepository, IRepository<Client> clientRepository, IRepository<Country> countryRepository)
    {
        this.tripRepository = tripRepository;
        this.clientRepository = clientRepository;
        this.countryRepository = countryRepository;
    }

    public async Task DeleteAsync(params object[] keys) => await this.tripRepository.DeleteAsync(keys);

    public async Task<IEnumerable<TripDto>> GetAllAsync() => (await this.tripRepository.GetAllAsync()).Select(this.MapToDto);

    public async Task<IEnumerable<TripDto>> GetOnPagesAsync(uint page, uint pageSize)
    {
        return (await this.tripRepository.GetQueryable().Skip((int)(page - 1)).Take((int)pageSize).ToListAsync()).Select(this.MapToDto);
    }

    public async Task<TripDto> InsertAsync(TripDto trip)
    {
        return this.MapToDto(await this.tripRepository.InsertAsync(this.MapFromDto(trip)));
    }

    public async Task InsertClientTripAsync(ClientTripPostRequestDto dto)
    {
        var trip = await this.tripRepository.GetByIdAsync(dto.IdTrip);

        if (await this.clientRepository.GetQueryable().AnyAsync(c => c.Pesel == dto.Pesel)) throw new ArgumentException(message: "Client with such pesel already exists.");

        if (trip.ClientTrips.Select(ct => ct.IdClientNavigation).Any(c => c.Pesel == dto.Pesel)) throw new ArgumentException(message: "Client with such pesel is already signed up for this trip.");

        if (dto.PaymentDate is not null && dto.PaymentDate.Value > DateTime.Now) throw new ArgumentException(message: "Payment date cannot be set to any date in the future.");

        var client = await this.clientRepository.InsertAsync(new() { FirstName = dto.FirstName, LastName = dto.LastName, Email = dto.Email, Telephone = dto.Telephone, Pesel = dto.Pesel });

        if (trip.DateFrom < DateTime.Today) throw new ArgumentException(message: "No one can be signed up for a trip that has already started.");

        trip.ClientTrips.Add(new() { IdClient = client.IdClient, IdTrip = trip.IdTrip, PaymentDate = dto.PaymentDate, RegisteredAt = DateTime.Now });

        await this.tripRepository.UpdateAsync(trip);
    }

    public Trip MapFromDto(TripDto dto)
    {
        Trip trip = new()
        {
            IdTrip = dto.IdTrip,
            Name = dto.Name,
            Description = dto.Description,
            DateFrom = dto.DateFrom,
            DateTo = dto.DateTo,
            MaxPeople = dto.MaxPeople
        };

        if (dto.Countries is not null) trip.IdCountries = dto.Countries.Select(c => this.countryRepository.GetQueryable().Where(co => c.Name == co.Name).Single()).ToList();

        return trip;
    }

    public TripDto MapToDto(Trip trip)
    {
        TripDto dto = new()
        {
            IdTrip = trip.IdTrip,
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople
        };

        if (trip.ClientTrips.Count > 0) dto.Clients = trip.ClientTrips.Select(ct => ct.IdClientNavigation).Select(c => new ClientDto() { FirstName = c.FirstName, LastName = c.LastName }).ToList();

        if (trip.IdCountries.Count > 0) dto.Countries = trip.IdCountries.Select(c => new CountryDto() { Name = c.Name }).ToList();

        return dto;
    }
}
