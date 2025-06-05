using apbd_cw12.Data;
using apbd_cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12.Repositories;

public class CountryRepository : IRepository<Country>
{
    protected readonly TripsContext database;

    public CountryRepository(TripsContext database) { this.database = database; }

    public async Task DeleteAsync(params object[] keys)
    {
        var country = await this.database.Countries.FindAsync(keys);

        if (country is null) throw new ArgumentNullException();

        this.database.Countries.Remove(country);
        await this.database.SaveChangesAsync();
    }

    public async Task<IEnumerable<Country>> GetAllAsync() => await this.database.Countries.ToListAsync();

    public async Task<Country> GetByIdAsync(params object[] keys) => await this.database.Countries.FindAsync(keys) ?? throw new ArgumentNullException();

    public IQueryable<Country> GetQueryable() => this.database.Countries.Include(c => c.IdTrips);

    public async Task<Country> InsertAsync(Country country)
    {
        await this.database.Countries.AddAsync(country);
        await this.database.SaveChangesAsync();
        return country;
    }

    public async Task<Country> UpdateAsync(Country country)
    {
        this.database.Countries.Update(country);
        await this.database.SaveChangesAsync();
        return country;
    }
}
