using apbd_cw12.Data;
using apbd_cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12.Repositories;

public class ClientTripRepository : IRepository<ClientTrip>
{
    protected readonly TripsContext database;

    public ClientTripRepository(TripsContext database) { this.database = database; }

    public async Task DeleteAsync(params object[] keys)
    {
        var clientTrip = await this.database.ClientTrips.FindAsync(keys);

        if (clientTrip is null) throw new ArgumentNullException();

        this.database.ClientTrips.Remove(clientTrip);
        await this.database.SaveChangesAsync();
    }

    public async Task<IEnumerable<ClientTrip>> GetAllAsync() => await this.database.ClientTrips.ToListAsync();

    public async Task<ClientTrip> GetByIdAsync(params object[] keys) => await this.database.ClientTrips.FindAsync(keys) ?? throw new ArgumentNullException();

    public IQueryable<ClientTrip> GetQueryable() => this.database.ClientTrips.Include(ct => ct.IdTripNavigation).Include(ct => ct.IdClientNavigation);

    public async Task<ClientTrip> InsertAsync(ClientTrip clientTrip)
    {
        await this.database.ClientTrips.AddAsync(clientTrip);
        await this.database.SaveChangesAsync();
        return clientTrip;
    }

    public async Task<ClientTrip> UpdateAsync(ClientTrip clientTrip)
    {
        this.database.ClientTrips.Update(clientTrip);
        await this.database.SaveChangesAsync();
        return clientTrip;
    }
}
