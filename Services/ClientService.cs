using apbd_cw12.DTOs;
using apbd_cw12.Models;
using apbd_cw12.Repositories;
using Microsoft.EntityFrameworkCore;

namespace apbd_cw12.Services;

public class ClientService : IService<Client, ClientDto>
{
    protected readonly IRepository<Client> repository;

    public ClientService(IRepository<Client> repository) { this.repository = repository; }

    public async Task DeleteAsync(params object[] keys)
    {
        var client = await this.repository.GetQueryable().Include(c => c.ClientTrips).Where(c => c.IdClient == this.repository.GetByIdAsync(keys).GetAwaiter().GetResult().IdClient).SingleAsync();

        if (client.ClientTrips.Count > 0) throw new ArgumentException(message: "Client cannot be deleted if they have any trips.");

        await this.repository.DeleteAsync(keys);
    }

    public async Task<IEnumerable<ClientDto>> GetAllAsync() => (await this.repository.GetAllAsync()).Select(this.MapToDto);

    public async Task<ClientDto> InsertAsync(ClientDto client) => this.MapToDto(await this.repository.InsertAsync(this.MapFromDto(client)));

    public Client MapFromDto(ClientDto dto) => new()
    {
        IdClient = dto.IdClient,
        FirstName = dto.FirstName,
        LastName = dto.LastName,
        Email = dto.Email,
        Telephone = dto.Telephone,
        Pesel = dto.Pesel
    };

    public ClientDto MapToDto(Client client) => new()
    {
        IdClient = client.IdClient,
        FirstName = client.FirstName,
        LastName = client.LastName,
        Email = client.Email,
        Telephone = client.Telephone,
        Pesel = client.Pesel
    };
}
