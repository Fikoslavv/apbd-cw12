using apbd_cw12.Data;
using apbd_cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12.Repositories;

public class ClientRepository : IRepository<Client>
{
    protected readonly TripsContext database;

    public ClientRepository(TripsContext database) { this.database = database; }

    public async Task DeleteAsync(params object[] keys)
    {
        var client = await this.database.Clients.FindAsync(keys);

        if (client is null) throw new ArgumentNullException();

        this.database.Clients.Remove(client);
        await this.database.SaveChangesAsync();
    }

    public async Task<IEnumerable<Client>> GetAllAsync() => await this.database.Clients.ToListAsync();

    public async Task<Client> GetByIdAsync(params object[] keys) => await this.database.Clients.FindAsync(keys) ?? throw new ArgumentNullException();

    public IQueryable<Client> GetQueryable() => this.database.Clients.Include(c => c.ClientTrips).ThenInclude(ct => ct.IdTripNavigation);

    public async Task<Client> InsertAsync(Client client)
    {
        await this.database.Clients.AddAsync(client);
        await this.database.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateAsync(Client client)
    {
        this.database.Clients.Update(client);
        await this.database.SaveChangesAsync();
        return client;
    }
}
