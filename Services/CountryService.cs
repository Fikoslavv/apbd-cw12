using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Repositories;

namespace apbd_cw12.Services;

public class CountryService : IService<Country, CountryDto>
{
    protected readonly IRepository<Country> repository;

    public CountryService(IRepository<Country> repository) { this.repository = repository; }

    public async Task DeleteAsync(params object[] keys) => await this.repository.DeleteAsync(keys);

    public async Task<IEnumerable<CountryDto>> GetAllAsync() => (await this.repository.GetAllAsync()).Select(this.MapToDto);

    public async Task<CountryDto> InsertAsync(CountryDto country) => this.MapToDto(await this.repository.InsertAsync(this.MapFromDto(country)));

    public Country MapFromDto(CountryDto dto) => new() { IdCountry = dto.IdCountry, Name = dto.Name };

    public CountryDto MapToDto(Country country) => new() { IdCountry = country.IdCountry, Name = country.Name };
}
