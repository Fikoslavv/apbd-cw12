using apbd_cw12.Data;
using apbd_cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12.Repositories;

public class TripRepository : IRepository<Trip>
{
    protected readonly TripsContext database;

    public TripRepository(TripsContext database) { this.database = database; }

    public async Task DeleteAsync(params object[] keys)
    {
        var trip = await this.database.Trips.FindAsync(keys);

        if (trip is null) throw new ArgumentNullException();

        this.database.Trips.Remove(trip);
        await this.database.SaveChangesAsync();
    }

    public async Task<IEnumerable<Trip>> GetAllAsync() => await this.database.Trips.ToListAsync();

    public async Task<Trip> GetByIdAsync(params object[] keys) => await this.database.Trips.FindAsync(keys) ?? throw new ArgumentNullException();

    public IQueryable<Trip> GetQueryable() => this.database.Trips.Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation).Include(t => t.IdCountries);

    public async Task<Trip> InsertAsync(Trip trip)
    {
        await this.database.Trips.AddAsync(trip);
        await this.database.SaveChangesAsync();
        return trip;
    }

    public async Task<Trip> UpdateAsync(Trip trip)
    {
        this.database.Trips.Update(trip);
        await this.database.SaveChangesAsync();
        return trip;
    }
}
